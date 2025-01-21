static class Tokenizer {
  // A generator/iterator that returns tokens one at a time, allowing peeking
  // without consuming the next token.
  public class Tokens {
    private readonly string input;
    private int position;
    private string currentToken;
    private string nextToken;

    internal Tokens(string input) {
      this.input = input;
      this.position = 0;
      this.currentToken = GetNextToken();
      this.nextToken = GetNextToken();
    }

    // Returns true if there are more tokens to consume
    public bool HasNext() {
      return currentToken != null;
    }

    // Returns the next token without consuming it
    public string Peek() {
      return currentToken;
    }

    // Consumes and returns the next token
    public string Consume() {
      string token = currentToken;
      currentToken = nextToken;
      nextToken = GetNextToken();
      return token;
    }

    private void Skip() {
      currentToken = nextToken;
      nextToken = GetNextToken();
    }

    private string GetNextToken() {
      // Skip whitespace
      while (position < input.Length && char.IsWhiteSpace(input[position])) {
        position++;
      }

      if (position >= input.Length) {
        return "";
      }

      // Start building a new token
      int start = position;
      char c = input[position];

      // Handle special characters
      if (IsSpecialChar(c)) {
        position++;
        return input[start].ToString();
      }

      // Handle numbers
      if (char.IsDigit(c)) {
        while (position < input.Length && char.IsDigit(input[position])) {
          position++;
        }
        return input[start..position];
      }

      // Handle identifiers and keywords
      if (char.IsLetter(c) || c == '_') {
        while (
          position < input.Length && 
          (char.IsLetterOrDigit(input[position]) || input[position] == '_')
        ) {
          position++;
        }
        return input[start..position];
      }

      // Handle operators
      if (IsOperator(c)) {
        while (position < input.Length && IsOperator(input[position])) {
          position++;
        }
        return input[start..position];
      }

      // Handle any other character as a single-character token
      position++;
      return input[start].ToString();
    }

    private bool IsSpecialChar(char c) {
      return (
        c == '(' || c == ')' || c == '{' || c == '}' ||
        c == '[' || c == ']' || c == ';' || c == ','
      );
    }

    private bool IsOperator(char c) {
      return (
        c == '+' || c == '-' || c == '*' || c == '/' ||  c == '=' || c == '<' ||
        c == '>' || c == '!' || c == '&' || c == '|' || c == '^'
      );
    }
  }

  // Returns a `Tokens` generator, given an input string
  public static Tokens Tokenize(string input) {
    return new Tokens(input);
  }
}