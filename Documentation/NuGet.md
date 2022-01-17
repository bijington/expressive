# Expressive

Expressive is a cross-platform expression parsing and evaluation framework. The cross-platform nature is achieved through compiling for `.NET Standard` so it will run on practically any platform.

Documentation on how to use the framework can be found [here](https://github.com/bijington/expressive/wiki).

## Usage Example

```csharp
var expression = new Expression("1+2");
var result = expression.Evaluate();
```

For further detail of usage please see the ([Usage wiki page](https://github.com/bijington/expressive/wiki/Usage))