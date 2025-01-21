Console.WriteLine("The Calculator™ v0.0.1");

while (true) {
  Console.Write(" > ");
  string userInput = Console.ReadLine() ?? "";

  Tokenizer.Tokens tokens = Tokenizer.Tokenize(userInput);
  while (tokens.HasNext()) {
    Console.WriteLine(tokens.Consume());
  }
}
