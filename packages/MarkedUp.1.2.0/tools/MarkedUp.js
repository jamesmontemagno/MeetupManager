/*!
* This file is meant to to be used within WinJS projects in order to ease the use of the MarkedUp SDK
**/

var MK = (function (mk) {

    "use strict";

    var client = mk.AnalyticClient;
    var navHandle = null;

    //error helper
    function serializeError(error) {

        //create the log object
        var ex = new mk.MKLog();

        if (!error) {

            //we got a null error object
            return null;

        }

        if (error && error.status && error.readyState) {

            //we probaly got a network request object
            ex.message = "Network Request with HTTP code " + error.status;
            ex.errorMessage = ex.message;
            ex.errorStackTrace = JSON.stringify(error);

            return ex;
        }
        
        if (error && error.detail && error.detail.errorMessage) {
            
            //we got something that looks like a structured error object
            ex.errorMessage = error.detail.errorMessage;
            ex.errorStackTrace = error.detail.errorStackTrace || error.detail.errorUrl;
            
            return ex;
        }

        //we got an object of indeterminate origin
        ex.message = error.message || "Recived a JavaScript Object (see stack trace for object JSON)";
        ex.errorMessage = error.message || JSON.stringify(error);
        ex.errorStackTrace = JSON.stringify(error);

        return ex;
    }

    //Log message helper
    function createLog(level, message, error, state) {

        var log;

        if (error) {

            //use helper to create log
            log = serializeError(error) || new mk.MKLog();
            log.level = level;
            log.message = message;

        } else {

            //we'll do it manually
            log = new mk.MKLog();
            log.level = level;
            log.message = message;
        }
        
        if (state !== null && state instanceof Windows.Foundation.Collections.PropertySet) {
            log.state = state;
        }

        return log;

    }

    //return object
    return {

        initialize: function (apiKeys) {

            try {
                if (!(apiKeys instanceof Array)) {
                    apiKeys = [apiKeys];
                }
                client.initialize(apiKeys);
            } catch (e) {

            }

        },
        
        /* logging functions */

        debug: function(message, error, state) {

            try {
                var log = createLog(mk.LogLevel.debug, message, error, state);
                client.log(log);
            } catch (e) {

            }

        },

        trace: function (message, error, state) {

            try {
                var log = createLog(mk.LogLevel.trace, message, error, state);
                client.log(log);
            } catch (e) {

            }

        },

        info: function (message, error, state) {

            try {
                var log = createLog(mk.LogLevel.info, message, error, state);
                client.log(log);
            } catch (e) {

            }

        },

        error: function (message, error, state) {

            try {
                var log = createLog(mk.LogLevel.error, message, error, state);
                client.log(log);
            } catch (e) {

            }

        },

        fatal: function (message, error, state) {

            try {
                var log = createLog(mk.LogLevel.fatal, message, error, state);
                client.log(log);
            } catch (e) {

            }

        },
        
        /* navigation */

        enterPage: function (page) {

            try {
                client.enterPage(page);
            } catch (e) {

            }

        },

        exitPage: function (page) {

            try {
                client.exitPage(page);
            } catch (e) {

            }

        },
        
        //Internal method for handling automatic navigation events
        _navigatedPage: function (navEvent) {
            try {
                if (navEvent && navEvent.detail && navEvent.detail.location) {
                    client.enterPage(navEvent.detail.location);
                }
            }
            catch (e) {

            }
        },

        //Automatically capture page change events
        registerNavigationFrame: function () {
            try {
                if (navHandle == null) {
                    var nav = WinJS.Navigation;
                    if (nav) {
                        navHandle = this._navigatedPage;
                        nav.addEventListener("navigated", navHandle);
                    }
                }
            }
            catch (e) {

            }
        },
        

        orientationChange: function (page, orientation) {

            try {
                client.orientationChanged(page, orientation);
            } catch (e) {

            }

        },
        
        /* commerce */
        
        //in-app purchase

        inAppPurchaseOfferCompleted: function (product)
        {
            try {
                if (product instanceof mk.InAppPurchase) {
                    client.inAppPurchaseComplete(product);
                } else if(typeof product === "string") {
                    console.log("This method is deprecated. Please see markedup.com/docs for updated commerce methods.");
                }
            } catch (e) {

            }
        },


        //trial
    
        trialConversionOfferCompleted: function (product)
        {
            try {
                if (typeof (product) !== "undefined") {
                    if (product instanceof mk.TrialConversion) {
                        client.trialConversionComplete(product);
                    }
                } else {
                    console.log("This method is deprecated. Please see markedup.com/docs for updated commerce methods.");
                }
                
            } catch (e) {

            }
        },
        
        /* custom session events */

        sessionEvent: function (eventName, parameters) {

            try {
                if (parameters !== null && parameters instanceof Windows.Foundation.Collections.PropertySet)
                    client.sessionEvent(eventName, parameters);
                else {
                    client.sessionEvent(eventName);
                }
            } catch (e) {

            }

        },
        
        /* last chance exception logging */
        
        logLastChanceException: function (error) {

            try {
                var log = createLog(mk.LogLevel.fatal, "An unhandled error occured in the app", error);
                client.logLastChanceException(log);
            } catch (e) {

            }
        },
        
        /* opt-out / opt-in - expects a boolean.
         * if true, then the user will be opted-out of MarkedUp Analytics.
         * if false, then the user will be able to opt-back-in to MarkedUp Analytics.
         * users are opted-in by default.
         */
        optOut: function(optOutPreference) {
            try {
                client.optOut(optOutPreference);
            } catch(e) {
                
            }
        }
    };

}(MarkedUp));