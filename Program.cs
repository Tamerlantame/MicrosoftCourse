

using MicrosoftCourse.Parser;
using System.Text;


var reader = new StringReader("2reader22+2a2");
List<Token> tokens = Tokenizer.Tokenize(reader).ToList();

Console.WriteLine("Hello, World!");
