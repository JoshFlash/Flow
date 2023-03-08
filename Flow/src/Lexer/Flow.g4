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
program: 'import' identifier ';' module_declaration statement* '}' EOF;

module_declaration: ('module' identifier)?;

statement: variable_declaration
         | assignment_statement
         | print_statement
         | if_statement
         | while_statement
         | for_statement
         | return_statement
         | function_call_statement
         | statement_block;

variable_declaration: 'let' identifier ':' type '=' variable_value ';';

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

type_parameter_list: type (',' type)*;

type_parameter: identifier;

assignment_statement: identifier ('[' expression ']')? '=' expression ';';

print_statement: 'Print' '(' expression ')' ';';

if_statement: 'if' expression statement_block ( 'else' statement_block )?;

while_statement: 'while' expression statement_block;

for_statement: 'for' identifier 'in' range_clause statement_block;

range_clause: expression '->' expression;

return_statement: 'return' expression? ';';

function_call_statement: identifier '(' argument_list? ')' ';';

argument_list: expression (',' expression)*;

statement_block: '{' statement* '}';

expression_value: literal
    | identifier
    | unary_operation
    | function_call_expression
    | '(' expression ')';

expression: expression_value
    | multiplicative_operation;

multiplicative_operation: expression_value ( MUL | DIV | MOD ) expression_value;

additive_operation: multiplicative_operation ( ADD | SUB ) multiplicative_operation;

relational_operation: additive_operation ( LT | LTE | GT | GTE ) additive_operation;

equality_operation: relational_operation ( EQ | NEQ ) relational_operation;

logical_and_operation: equality_operation AND equality_operation;

logical_or_operation: logical_and_operation OR logical_and_operation;

literal: INTEGER
    | TRUE
    | FALSE
    | STRING;

unary_operation: NOT expression;

binary_operation: expression ( ADD | SUB | MUL | DIV | MOD | EQ | NEQ | LT | LTE | GT | GTE | AND | OR ) expression;

function_call_expression: identifier '(' argument_list? ')';

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

// TODO: Add operator precedence to binary_operation rule.
