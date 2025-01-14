Change log
==========

## December 3, 2015 - v1.6.0
 * Added support of “Edge” JsRT version of Chakra JavaScript engine
 * `ChakraJsRt` mode was renamed to `ChakraIeJsRt`
 
## July 23, 2015 - v1.5.6
 * Source code of the `ChakraJsRtJsEngine` was synchronized with the Chakra Sample Hosts version of July 11, 2015

## June 29, 2015 - v1.5.5
 * Fixed an error, that occurs on computers with IE 6
 * Removed `Obsolete` attribute from parameterless constructor

## June 28, 2015 - v1.5.4
 * In `ChakraActiveScript` mode added native support of ECMAScript 5 (without polyfills)
 * Added `JsEngineSettings` class for any reason in the future to abandon redundant constructors

## May 5, 2015 - v1.5.3
 * JSON2 library was updated to version of May 3, 2015

## April 5, 2015 - v1.5.2
 * JSON2 library was updated to version of February 25, 2015

## January 13, 2015 - v1.5.1
 * In ECMAScript 5 Polyfill added polyfill for the `String.prototype.split` method

## October 12, 2014 - v1.5.0
 * Removed dependency on `System.Web.Extensions`
 * Assembly is now targeted on the .NET Framework 4 Client Profile

## July 22, 2014 - v1.4.4
 * Source code of the `ChakraJsRtJsEngine` was synchronized with the Chakra Sample Hosts version of July 22, 2014

## April 27, 2014 - v1.4.3
 * In solution was enabled NuGet package restore
 * Fixed [JavaScriptEngineSwitcher.Msie's bug #7](https://github.com/Taritsyn/JavaScriptEngineSwitcher/issues/7) "MsieJavaScriptEngine.ActiveScript.ActiveScriptException not wrapped"

## March 24, 2014 - v1.4.2
 * Fixed [JavaScriptEngineSwitcher.Msie's bug #5](http://github.com/Taritsyn/JavaScriptEngineSwitcher/issues/5) "MSIE "Catastrophic failure" when disposing"

## March 22, 2014 - v1.4.1
 * Fixed minor bugs

## February 27, 2014 - v1.4.0
 * Removed following methods: `HasProperty`, `GetPropertyValue`, `SetPropertyValue` and `RemoveProperty`
 * Fixed [bug #3](http://github.com/Taritsyn/MsieJavaScriptEngine/issues/3) "execute code from different threads"
 * Now in the `ChakraJsRt` mode is available a more detailed information about errors
 * In ECMAScript 5 Polyfill improved a performance of the `String.prototype.trim` method
 * JSON2 library was updated to version of February 4, 2014

## January 16, 2014 - v1.3.0
 * Added support of the JsRT version of Chakra
 * Now the MSIE JavaScript Engine can work in 4 modes: `Auto` (selected by default), `Classic`, `ChakraActiveScript` and `ChakraJsRt`
 * Following methods are obsolete: `HasProperty`, `GetPropertyValue`, `SetPropertyValue` and `RemoveProperty`

## December 30, 2013 - v1.2.0
 * Fixed errors in ECMAScript 5 Polyfill
 * Added support of JavaScript `undefined` type

## September 3, 2013 - v1.1.3
 * Access modifier of the `JsEngineLoadException` class has changed to public

## June 20, 2013 - v1.1.2
 * JSON2 library was updated to version of May 26, 2013

## October 15, 2012 - v1.1.1
 * Assembly `MsieJavaScriptEngine.dll` now signed

## October 11, 2012 - v1.1.0
 * Added ability of using the Douglas Crockford's [JSON2](http://github.com/douglascrockford/JSON-js) library
 * By default using of the JSON2 library is disabled

## September 21, 2012 - v1.0.8
 * Changed the format of error messages

## September 9, 2012 - v1.0.7
 * Added the `ActiveScriptErrorFormatter` class

## August 29, 2012 - v1.0.5
 * [JavaScript Array Polyfills from TutorialsPoint.com](http://www.tutorialspoint.com/javascript/) was replaced by the Douglas Crockford's [ECMAScript 5 Polyfill](http://nuget.org/packages/ES5)
 * By default using of the ECMAScript 5 Polyfill is disabled

## August 27, 2012 - v1.0.1
 * Added the `JsEngineLoadException` class

## August 26, 2012 - v1.0.0
 * Initial version uploaded