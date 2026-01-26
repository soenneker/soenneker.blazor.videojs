export class VideoJsInterop {
    constructor() {
        this.players = new Map();
        this.listeners = new Map();
    }

    create(element, elementId, configJson) {
        if (!window.videojs) {
            throw new Error("Video.js is not available. Ensure the script is loaded.");
        }

        const options = configJson ? JSON.parse(configJson) : {};

        if (this.players.has(elementId)) {
            const existing = this.players.get(elementId);
            if (existing && !existing.isDisposed()) {
                existing.dispose();
            }
            this.players.delete(elementId);
        }

        const player = window.videojs(element, options);
        this.players.set(elementId, player);
    }

    updateSources(elementId, sources) {
        const player = this.players.get(elementId);
        if (!player) return;
        player.src(sources || []);
    }

    setPoster(elementId, poster) {
        const player = this.players.get(elementId);
        if (!player) return;
        player.poster(poster || "");
    }

    registerEvent(elementId, eventName, dotNetReference, callbackMethod) {
        const player = this.players.get(elementId);
        if (!player) return;

        if (!this.listeners.has(elementId)) {
            this.listeners.set(elementId, new Map());
        }

        const elementListeners = this.listeners.get(elementId);
        if (elementListeners.has(eventName)) {
            player.off(eventName, elementListeners.get(eventName));
            elementListeners.delete(eventName);
        }

        const handler = () => {
            if (dotNetReference) {
                dotNetReference.invokeMethodAsync(callbackMethod, eventName);
            }
        };

        elementListeners.set(eventName, handler);

        if (eventName === "ready" && typeof player.ready === "function") {
            player.ready(handler);
        } else {
            player.on(eventName, handler);
        }
    }

    dispose(elementId) {
        const player = this.players.get(elementId);
        if (!player) return;

        if (this.listeners.has(elementId)) {
            const elementListeners = this.listeners.get(elementId);
            elementListeners.forEach((handler, eventName) => {
                player.off(eventName, handler);
            });
            this.listeners.delete(elementId);
        }

        if (!player.isDisposed()) {
            player.dispose();
        }
        this.players.delete(elementId);
    }
}

window.VideoJsInterop = new VideoJsInterop();
