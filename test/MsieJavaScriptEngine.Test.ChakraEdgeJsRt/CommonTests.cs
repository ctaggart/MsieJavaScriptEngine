namespace MsieJavaScriptEngine.Test.ChakraEdgeJsRt
{
	using NUnit.Framework;

	using MsieJavaScriptEngine;
	using Common;

	[TestFixture]
	public class CommonTests : CommonTestsBase
	{
		protected override MsieJsEngine CreateJsEngine()
		{
			var jsEngine = new MsieJsEngine(new JsEngineSettings
			{
				EngineMode = JsEngineMode.ChakraEdgeJsRt,
				UseEcmaScript5Polyfill = false,
				UseJson2Library = false
			});

			return jsEngine;
		}

        //var person = { firstName:"John", lastName:"Doe", age:50, eyeColor:"blue" };
        [Test]
        public virtual void PassJsRefToFunction()
        {
            // Arrange
            const string input = "'Hello, ' + \"Vasya\" + '?';";
            const string targetOutput = "Hello, Vasya?";

            // Act
            string output;

            using (var jsEngine = CreateJsEngine())
            {
                jsEngine.Execute(@"function create(){ return { firstName: ""John"", lastName: ""Doe"", age: 50, eyeColor: ""blue"" } };");
                var person = jsEngine.CallFunction("create");
                System.Diagnostics.Debug.WriteLine("person is " + person);
                output = jsEngine.Evaluate<string>(input);
            }

            // Assert
            Assert.AreEqual(targetOutput, output);
        }
    }
}