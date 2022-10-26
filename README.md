# MicrosoftCourse
In computer science, the shunting yard algorithm is a method for parsing arithmetical or logical expressions, or a combination of both, specified in infix notation. It can produce either a postfix notation string, also known as Reverse Polish notation (RPN), or an abstract syntax tree (AST)

## A simple conversion
- Input: 3 + 4
- Push 3 to the output queue (whenever a number is read it is pushed to the output)
- Push + (or its ID) onto the operator stack
- Push 4 to the output queue
- After reading the expression, pop the operators off the stack and add them to the output.
- In this case there is only one, "+".
- Output: 3 4 +
## Graphical illustration
<p align="center">
<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/2/24/Shunting_yard.svg/800px-Shunting_yard.svg.png">
</p>