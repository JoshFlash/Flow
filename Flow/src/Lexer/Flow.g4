grammar Flow;

/* Lexer Rules */
WS: [ \t\r\n]+ -> skip;

TRUE: 'true';
FALSE: 'false';

ADD: '+';
SUB: '-';
MUL: '*';
DIV: '/';
MOD: 'mod';

EQ: 'is';
NEQ: 'is not';
LT: '<';
LTE: '<=';
GT: '>';
GTE: '>=';
AND: 'and';
OR: 'or';
NOT: 'not';

LPAREN: '(';
RPAREN: ')';
LBRACE: '{';
RBRACE: '}';
LBRACK: '[';
RBRACK: ']';
COMMA: ',';
SEMICOLON: ';';
COLON: ':';
ARROW: '->';

INTEGER: [0-9]+;
IDENTIFIER: [a-zA-Z_][a-zA-Z_0-9]*;

// Comment
COMMENT: '//' ~[\r\n]* -> skip;
BLOCK_COMMENT: '#' .*? '#' -> skip;

/* Parser Rules */
program: import_list module_declaration statement* '}' EOF;

import_list: import_statement*;
import_statement: 'import' identifier ';';

module_declaration: ('module' identifier statement '}')?;

statement: constant_declaration
         | variable_declaration
         | assignment_statement
         | print_statement
         | if_statement
         | while_statement
         | for_statement
         | return_statement
         | function_call_statement
         | statement_block
         | function_declaration
         | element_assignment_statement;

variable_declaration: 'var' identifier ':' type '=' variable_value ';';
constant_declaration: 'const' identifier ':' type '=' variable_value ';';

variable_value: expression
              | 'array' '[' type ']' '(' expression ')'
              | 'map' '[' type ',' type ']' '(' ')';

type: 'int'
    | 'bool'
    | 'string'
    | 'array' '[' type ']'
    | 'map' '[' type ',' type ']'
    | generic_type;

generic_type: identifier '<' type_parameter_list '>';
type_parameter_list: type_parameter (',' type_parameter)*;
type_parameter: identifier;

assignment_statement: identifier '=' expression ';';
element_assignment_statement: element_access_expression '=' expression ';';

print_statement: 'Print' '(' expression ')' ';';
if_statement: 'if' expression statement_block ( 'else' statement_block )?;
while_statement: 'while' expression statement_block;
for_statement: 'for' identifier 'in' range_clause ('where' expression)? statement_block;
return_statement: 'return' expression? ';';
function_call_statement: identifier '(' argument_list? ')' ';';

range_clause: expression ARROW expression;

for_expression: 'for' identifier 'in' range_clause ('where' expression)?;

argument_list: expression (',' expression)*;

statement_block: '{' statement* '}';

function_declaration: 'let' identifier parameter_list? ':' type? '=' statement_block;
parameter_list: '(' (parameter (',' parameter)*)? ')';
parameter: identifier ':' type;

unary_operation: NOT expression;

expression: logical_or;
logical_or: logical_and (OR logical_and)*;
logical_and: equality (AND equality)*;
equality: relational ((EQ | NEQ) relational)*;
relational: additive ((LT | LTE | GT | GTE) additive)*;
additive: multiplicative ((ADD | SUB) multiplicative)*;
multiplicative: expression_value ((MUL | DIV | MOD) expression_value)*;

expression_value: literal
    | identifier
    | unary_operation
    | function_call_expression
    | element_access_expression
    | '(' expression ')';

function_call_expression: identifier '(' argument_list? ')';
element_access_expression: identifier '[' expression ']';

literal: INTEGER
    | TRUE
    | FALSE
    | STRING;

/* Lexer Helpers */
identifier: IDENTIFIER;

STRING: '"' ( ~["\\] | '\\' . )* '"';

/* Error Tokens */
ERR_CHAR: .; // Match any character except EOF
ERR_TOKEN: ~[\r\n]; // Match any token except newline

/* Operator Precedence */
// Lowest to highest
// Primary -> Unary -> Multiplicative -> Additive -> Relational -> Equality -> Logical And -> Logical Or
// Primary: Parentheses, literals, identifiers, function calls
// Unary: NOT
// Multiplicative: *, /, %
// Additive: +, -
// Relational: <, <=, >, >=
// Equality: is, is not
// Logical And: and
// Logical Or: or
