﻿namespace MsieJavaScriptEngine.Test.Common
{
	using System;
	using System.IO;
	using System.Text.RegularExpressions;

	using NUnit.Framework;

	using MsieJavaScriptEngine;

	[TestFixture]
	public abstract class CommonTestsBase
	{
		/// <summary>
		/// Regular expression for working with the `bin` directory path
		/// </summary>
		private readonly Regex _binDirRegex = new Regex(@"\\bin\\(?:Debug|Release)\\?$", RegexOptions.IgnoreCase);


		protected abstract MsieJsEngine CreateJsEngine();

		#region Evaluation of code

		[Test]
		public virtual void EvaluationOfExpressionWithUndefinedResultIsCorrect()
		{
			// Arrange
			const string input = "undefined";
			var targetOutput = Undefined.Value;

			// Act
			object output;

			using (var jsEngine = CreateJsEngine())
			{
				output = jsEngine.Evaluate(input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void EvaluationOfExpressionWithNullResultIsCorrect()
		{
			// Arrange
			const string input = "null";
			const object targetOutput = null;

			// Act
			object output;

			using (var jsEngine = CreateJsEngine())
			{
				output = jsEngine.Evaluate(input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void EvaluationOfExpressionWithBooleanResultIsCorrect()
		{
			// Arrange
			const string input1 = "7 > 5";
			const bool targetOutput1 = true;

			const string input2 = "null === undefined";
			const bool targetOutput2 = false;

			// Act
			bool output1;
			bool output2;

			using (var jsEngine = CreateJsEngine())
			{
				output1 = jsEngine.Evaluate<bool>(input1);
				output2 = jsEngine.Evaluate<bool>(input2);
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
		}

		[Test]
		public virtual void EvaluationOfExpressionWithIntegerResultIsCorrect()
		{
			// Arrange
			const string input = "7 * 8 - 20";
			const int targetOutput = 36;

			// Act
			int output;

			using (var jsEngine = CreateJsEngine())
			{
				output = jsEngine.Evaluate<int>(input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void EvaluationOfExpressionWithDoubleResultIsCorrect()
		{
			// Arrange
			const string input = "Math.PI + 0.22";
			const double targetOutput = 3.36;

			// Act
			double output;

			using (var jsEngine = CreateJsEngine())
			{
				output = Math.Round(jsEngine.Evaluate<double>(input), 2);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void EvaluationOfExpressionWithStringResultIsCorrect()
		{
			// Arrange
			const string input = "'Hello, ' + \"Vasya\" + '?';";
			const string targetOutput = "Hello, Vasya?";

			// Act
			string output;

			using (var jsEngine = CreateJsEngine())
			{
				output = jsEngine.Evaluate<string>(input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		#endregion

		#region Execution of code

		[Test]
		public virtual void ExecutionOfCodeIsCorrect()
		{
			// Arrange
			const string functionCode = @"function add(num1, num2) {
				return (num1 + num2);
			}";
			const string input = "add(7, 9);";
			const int targetOutput = 16;

			// Act
			int output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = jsEngine.Evaluate<int>(input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void ExecutionOfFileIsCorrect()
		{
			// Arrange
			string relativeFilePath = "MsieJavaScriptEngine.Test.Common/Resources/square.js";
			string appBaseDirPath = AppDomain.CurrentDomain.BaseDirectory;
			if (_binDirRegex.IsMatch(appBaseDirPath))
			{
				relativeFilePath = Path.Combine("..\\..\\..\\", relativeFilePath);
			}
			string absoluteFilePath = Path.GetFullPath(Path.Combine(appBaseDirPath, relativeFilePath));
			const string input = "square(6);";
			const int targetOutput = 36;

			// Act
			int output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.ExecuteFile(absoluteFilePath);
				output = jsEngine.Evaluate<int>(input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void ExecutionOfResourceByTypeIsCorrect()
		{
			// Arrange
			const string resourceName = "MsieJavaScriptEngine.Test.Common.Resources.cube.js";
			const string input = "cube(5);";
			const int targetOutput = 125;

			// Act
			int output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.ExecuteResource(resourceName, typeof(CommonTestsBase));
				output = jsEngine.Evaluate<int>(input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void ExecutionOfResourceByAssemblyIsCorrect()
		{
			// Arrange
			const string resourceName = "MsieJavaScriptEngine.Test.Common.Resources.power.js";
			const string input = "power(4, 3);";
			const int targetOutput = 64;

			// Act
			int output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.ExecuteResource(resourceName, typeof(CommonTestsBase).Assembly);
				output = jsEngine.Evaluate<int>(input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		#endregion

		#region Calling of functions

		[Test]
		public virtual void CallingOfFunctionWithoutParametersIsCorrect()
		{
			// Arrange
			const string functionCode = @"function hooray() {
	return 'Hooray!';
}";
			const string targetOutput = "Hooray!";

			// Act
			string output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = (string) jsEngine.CallFunction("hooray");
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithUndefinedResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function testUndefined(value) {
	if (typeof value !== 'undefined') {
		throw new TypeError();
	}

	return undefined;
}";
			object input = Undefined.Value;

			// Act
			object output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = jsEngine.CallFunction("testUndefined", input);
			}

			// Assert
			Assert.AreEqual(input, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithNullResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function testNull(value) {
	if (value !== null) {
		throw new TypeError();
	}

	return null;
}";
			const object input = null;

			// Act
			object output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = jsEngine.CallFunction("testNull", input);
			}

			// Assert
			Assert.AreEqual(input, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithBooleanResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function inverse(value) {
	return !value;
}";
			const bool input = false;
			const bool targetOutput = true;

			// Act
			bool output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = jsEngine.CallFunction<bool>("inverse", input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithIntegerResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function negate(value) {
	return -1 * value;
}";
			const int input = 28;
			const int targetOutput = -28;

			// Act
			int output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = jsEngine.CallFunction<int>("negate", input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithDoubleResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function triple(value) {
	return 3 * value;
}";
			const double input = 3.2;
			const double targetOutput = 9.6;

			// Act
			double output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = Math.Round(jsEngine.CallFunction<double>("triple", input), 1);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithStringResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function greeting(name) {
	return 'Hello, ' + name + '!';
}";
			const string input = "Vovan";
			const string targetOutput = "Hello, Vovan!";

			// Act
			string output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = jsEngine.CallFunction<string>("greeting", input);
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithManyParametersIsCorrect()
		{
			// Arrange
			const string functionCode = @"function determineArgumentsTypes() {
	var result = '',
		argumentIndex,
		argumentCount = arguments.length
		;

	for (argumentIndex = 0; argumentIndex < argumentCount; argumentIndex++) {
		if (argumentIndex > 0) {
			result += ', ';
		}
		result += typeof arguments[argumentIndex];
	}

	return result;
}";

			// Act
			string output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = (string) jsEngine.CallFunction("determineArgumentsTypes", Undefined.Value, null,
					true, 12, 3.14, "test");
			}

			// Assert
			Assert.AreEqual("undefined, object, boolean, number, number, string", output);
		}

		[Test]
		public virtual void CallingOfFunctionWithManyParametersAndBooleanResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function and() {
	var result = null,
		argumentIndex,
		argumentCount = arguments.length,
		argumentValue
		;

	for (argumentIndex = 0; argumentIndex < argumentCount; argumentIndex++) {
		argumentValue = arguments[argumentIndex];

		if (result !== null) {
			result = result && argumentValue;
		}
		else {
			result = argumentValue;
		}
	}

	return result;
}";

			// Act
			bool output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = jsEngine.CallFunction<bool>("and", true, true, false, true);
			}

			// Assert
			Assert.AreEqual(false, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithManyParametersAndIntegerResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function sum() {
	var result = 0,
		argumentIndex,
		argumentCount = arguments.length
		;

	for (argumentIndex = 0; argumentIndex < argumentCount; argumentIndex++) {
		result += arguments[argumentIndex];
	}

	return result;
}";

			// Act
			int output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = jsEngine.CallFunction<int>("sum", 120, 5, 18, 63);
			}

			// Assert
			Assert.AreEqual(206, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithManyParametersAndDoubleResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function sum() {
	var result = 0,
		argumentIndex,
		argumentCount = arguments.length
		;

	for (argumentIndex = 0; argumentIndex < argumentCount; argumentIndex++) {
		result += arguments[argumentIndex];
	}

	return result;
}";

			// Act
			double output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = Math.Round(jsEngine.CallFunction<double>("sum", 22000, 8.5, 0.05, 3), 2);
			}

			// Assert
			Assert.AreEqual(22011.55, output);
		}

		[Test]
		public virtual void CallingOfFunctionWithManyParametersAndStringResultIsCorrect()
		{
			// Arrange
			const string functionCode = @"function concatenate() {
	var result = '',
		argumentIndex,
		argumentCount = arguments.length
		;

	for (argumentIndex = 0; argumentIndex < argumentCount; argumentIndex++) {
		result += arguments[argumentIndex];
	}

	return result;
}";

			// Act
			string output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.Execute(functionCode);
				output = jsEngine.CallFunction<string>("concatenate", "Hello", ",", " ", "Petya", "!");
			}

			// Assert
			Assert.AreEqual("Hello, Petya!", output);
		}

		#endregion

		#region Getting, setting and removing variables

		[Test]
		public virtual void SettingAndGettingVariableWithUndefinedValueIsCorrect()
		{
			// Arrange
			const string variableName = "myVar1";
			object input = Undefined.Value;

			// Act
			bool variableExists;
			object output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.SetVariableValue(variableName, input);
				variableExists = jsEngine.HasVariable(variableName);
				output = jsEngine.GetVariableValue(variableName);
			}

			// Assert
			Assert.IsFalse(variableExists);
			Assert.AreEqual(input, output);
		}

		[Test]
		public virtual void SettingAndGettingVariableWithNullValueIsCorrect()
		{
			// Arrange
			const string variableName = "myVar2";
			const object input = null;

			// Act
			bool variableExists;
			object output;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.SetVariableValue(variableName, input);
				variableExists = jsEngine.HasVariable(variableName);
				output = jsEngine.GetVariableValue(variableName);
			}

			// Assert
			Assert.IsTrue(variableExists);
			Assert.AreEqual(input, output);
		}

		[Test]
		public virtual void SettingAndGettingVariableWithBooleanValueIsCorrect()
		{
			// Arrange
			const string variableName = "isVisible";

			const bool input1 = true;
			const bool targetOutput1 = false;

			const bool input2 = true;

			// Act
			bool variableExists;
			bool output1;
			bool output2;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.SetVariableValue(variableName, input1);
				variableExists = jsEngine.HasVariable(variableName);
				jsEngine.Execute(string.Format("{0} = !{0};", variableName));
				output1 = jsEngine.GetVariableValue<bool>(variableName);

				jsEngine.SetVariableValue(variableName, input2);
				output2 = jsEngine.GetVariableValue<bool>(variableName);
			}

			// Assert
			Assert.IsTrue(variableExists);
			Assert.AreEqual(targetOutput1, output1);

			Assert.AreEqual(input2, output2);
		}

		[Test]
		public virtual void SettingAndGettingVariableWithIntegerValueIsCorrect()
		{
			// Arrange
			const string variableName = "amount";

			const int input1 = 38;
			const int targetOutput1 = 41;

			const int input2 = 711;

			// Act
			bool variableExists;
			int output1;
			int output2;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.SetVariableValue(variableName, input1);
				variableExists = jsEngine.HasVariable(variableName);
				jsEngine.Execute(string.Format("{0} += 3;", variableName));
				output1 = jsEngine.GetVariableValue<int>(variableName);

				jsEngine.SetVariableValue(variableName, input2);
				output2 = jsEngine.GetVariableValue<int>(variableName);
			}

			// Assert
			Assert.IsTrue(variableExists);
			Assert.AreEqual(targetOutput1, output1);

			Assert.AreEqual(input2, output2);
		}

		[Test]
		public virtual void SettingAndGettingVariableWithDoubleValueIsCorrect()
		{
			// Arrange
			const string variableName = "price";

			const double input1 = 2.20;
			const double targetOutput1 = 2.17;

			const double input2 = 3.50;

			// Act
			bool variableExists;
			double output1;
			double output2;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.SetVariableValue(variableName, input1);
				variableExists = jsEngine.HasVariable(variableName);
				jsEngine.Execute(string.Format("{0} -= 0.03;", variableName));
				output1 = Math.Round(jsEngine.GetVariableValue<double>(variableName), 2);

				jsEngine.SetVariableValue(variableName, input2);
				output2 = Math.Round(jsEngine.GetVariableValue<double>(variableName), 2);
			}

			// Assert
			Assert.IsTrue(variableExists);
			Assert.AreEqual(targetOutput1, output1);

			Assert.AreEqual(input2, output2);
		}

		[Test]
		public virtual void SettingAndGettingVariableWithStringValueIsCorrect()
		{
			// Arrange
			const string variableName = "word";

			const string input1 = "Hooray";
			const string targetOutput1 = "Hooray!";

			const string input2 = "Hurrah";

			// Act
			bool variableExists;
			string output1;
			string output2;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.SetVariableValue(variableName, input1);
				variableExists = jsEngine.HasVariable(variableName);
				jsEngine.Execute(string.Format("{0} += '!';", variableName));
				output1 = jsEngine.GetVariableValue<string>(variableName);

				jsEngine.SetVariableValue(variableName, input2);
				output2 = jsEngine.GetVariableValue<string>(variableName);
			}

			// Assert
			Assert.IsTrue(variableExists);
			Assert.AreEqual(targetOutput1, output1);

			Assert.AreEqual(input2, output2);
		}

		[Test]
		public virtual void RemovingVariableIsCorrect()
		{
			// Arrange
			const string variableName = "price";
			const double input = 120.55;

			// Act
			bool variableBeforeRemovingExists;
			bool variableAfterRemovingExists;

			using (var jsEngine = CreateJsEngine())
			{
				jsEngine.SetVariableValue(variableName, input);
				variableBeforeRemovingExists = jsEngine.HasVariable(variableName);
				jsEngine.RemoveVariable(variableName);
				variableAfterRemovingExists = jsEngine.HasVariable(variableName);
			}

			// Assert
			Assert.IsTrue(variableBeforeRemovingExists);
			Assert.IsFalse(variableAfterRemovingExists);
		}

		#endregion
	}
}