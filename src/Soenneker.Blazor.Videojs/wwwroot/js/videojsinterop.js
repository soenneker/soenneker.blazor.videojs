const players = new Map();
const listeners = new Map();

export function create(element, elementId, configJson) {
    if (!window.videojs) {
        throw new Error("Video.js is not available. Ensure the script is loaded.");
    }

    const options = configJson ? JSON.parse(configJson) : {};

    if (players.has(elementId)) {
        const existing = players.get(elementId);
        if (existing && !existing.isDisposed()) {
            existing.dispose();
        }
        players.delete(elementId);
    }

    const player = window.videojs(element, options);
    players.set(elementId, player);
}

export function updateSources(elementId, sources) {
    const player = players.get(elementId);
    if (!player) return;
    player.src(sources || []);
}

export function setPoster(elementId, poster) {
    const player = players.get(elementId);
    if (!player) return;
    player.poster(poster || "");
}

export function registerEvent(elementId, eventName, dotNetReference, callbackMethod) {
    const player = players.get(elementId);
    if (!player) return;

    if (!listeners.has(elementId)) {
        listeners.set(elementId, new Map());
    }

    const elementListeners = listeners.get(elementId);
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

export function dispose(elementId) {
    const player = players.get(elementId);
    if (!player) return;

    if (listeners.has(elementId)) {
        const elementListeners = listeners.get(elementId);
        elementListeners.forEach((handler, eventName) => {
            player.off(eventName, handler);
        });
        listeners.delete(elementId);
    }

    if (!player.isDisposed()) {
        player.dispose();
    }

    players.delete(elementId);
}
