const players = new Map();
const listeners = new Map();

function getPlayer(elementId) {
    const player = players.get(elementId);

    if (!player || player.isDisposed()) {
        throw new Error(`Video.js player was not found: ${elementId}`);
    }

    return player;
}

function removeListeners(elementId, player) {
    const elementListeners = listeners.get(elementId);

    if (!elementListeners) {
        return;
    }

    if (player && !player.isDisposed()) {
        elementListeners.forEach((handler, eventName) => player.off(eventName, handler));
    }

    listeners.delete(elementId);
}

export function create(element, elementId, configJson) {
    if (!window.videojs) {
        throw new Error("Video.js is not available. Ensure the script is loaded.");
    }

    const options = configJson ? JSON.parse(configJson) : {};

    if (players.has(elementId)) {
        const existing = players.get(elementId);
        removeListeners(elementId, existing);

        if (existing && !existing.isDisposed()) {
            existing.dispose();
        }
        players.delete(elementId);
    }

    const player = window.videojs(element, options);
    players.set(elementId, player);
}

export function updateSources(elementId, sources) {
    const player = getPlayer(elementId);
    player.src(sources || []);
}

export function setPoster(elementId, poster) {
    const player = getPlayer(elementId);
    player.poster(poster || "");
}

export function registerEvent(elementId, eventName, dotNetReference, callbackMethod) {
    const player = getPlayer(elementId);

    if (!listeners.has(elementId)) {
        listeners.set(elementId, new Map());
    }

    const elementListeners = listeners.get(elementId);
    if (elementListeners.has(eventName)) {
        player.off(eventName, elementListeners.get(eventName));
        elementListeners.delete(eventName);
    }

    const handler = () => {
        dotNetReference.invokeMethodAsync(callbackMethod, eventName).catch(() => { });
    };

    elementListeners.set(eventName, handler);

    if (eventName === "ready" && typeof player.ready === "function") {
        player.ready(handler);
    } else {
        player.on(eventName, handler);
    }
}

export function unregisterEvent(elementId, eventName) {
    const player = getPlayer(elementId);
    const elementListeners = listeners.get(elementId);

    if (!elementListeners) {
        return;
    }

    const handler = elementListeners.get(eventName);

    if (!handler) {
        return;
    }

    player.off(eventName, handler);
    elementListeners.delete(eventName);

    if (elementListeners.size === 0) {
        listeners.delete(elementId);
    }
}

export function dispose(elementId) {
    const player = players.get(elementId);
    removeListeners(elementId, player);

    if (player && !player.isDisposed()) {
        player.dispose();
    }

    players.delete(elementId);
}
