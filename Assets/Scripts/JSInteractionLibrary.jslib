var JSInteractionLibrary = {
    $jsInteractionManager: {
        eventListener: null,
    },

    /**
     * Set event listener for Unity
     *
     * @param callback Reference to C# static function
     */
    SetJSEventListener: function (callback) {
        console.log("SetJSEventListener called.");
        jsInteractionManager.eventListener = callback;
    },

    /**
     * Call method from Unity
     *
     * @param actionPtr Pointer to action string
     * @param messagePtr Pointer to message string
     */
    CallJSMethod: function (actionPtr, messagePtr) {
        var action = UTF8ToString(actionPtr);
        var message = UTF8ToString(messagePtr);

        console.log("Called from Unity with action: " + action + " and message: " + message);
        Module.dynCall_v(jsInteractionManager.eventListener);
    },
};

autoAddDeps(JSInteractionLibrary, '$jsInteractionManager');
mergeInto(LibraryManager.library, JSInteractionLibrary);
