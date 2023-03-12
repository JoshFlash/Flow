// Generated from c:\Users\joshu\Projects\Flow\Flow\src\Lexer\Flow.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class FlowLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		WS=18, TRUE=19, FALSE=20, ADD=21, SUB=22, MUL=23, DIV=24, MOD=25, EQ=26, 
		NEQ=27, LT=28, LTE=29, GT=30, GTE=31, AND=32, OR=33, NOT=34, LPAREN=35, 
		RPAREN=36, LBRACE=37, RBRACE=38, LBRACK=39, RBRACK=40, COMMA=41, SEMICOLON=42, 
		COLON=43, ARROW=44, INTEGER=45, IDENTIFIER=46, COMMENT=47, BLOCK_COMMENT=48, 
		STRING=49, ERR_CHAR=50, ERR_TOKEN=51;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
			"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
			"WS", "TRUE", "FALSE", "ADD", "SUB", "MUL", "DIV", "MOD", "EQ", "NEQ", 
			"LT", "LTE", "GT", "GTE", "AND", "OR", "NOT", "LPAREN", "RPAREN", "LBRACE", 
			"RBRACE", "LBRACK", "RBRACK", "COMMA", "SEMICOLON", "COLON", "ARROW", 
			"INTEGER", "IDENTIFIER", "COMMENT", "BLOCK_COMMENT", "STRING", "ERR_CHAR", 
			"ERR_TOKEN"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'import'", "'module'", "'var'", "'='", "'array'", "'map'", "'int'", 
			"'bool'", "'string'", "'Print'", "'if'", "'else'", "'while'", "'for'", 
			"'in'", "'return'", "'let'", null, "'true'", "'false'", "'+'", "'-'", 
			"'*'", "'/'", "'mod'", "'is'", "'is not'", "'<'", "'<='", "'>'", "'>='", 
			"'and'", "'or'", "'not'", "'('", "')'", "'{'", "'}'", "'['", "']'", "','", 
			"';'", "':'", "'->'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, "WS", "TRUE", "FALSE", "ADD", "SUB", 
			"MUL", "DIV", "MOD", "EQ", "NEQ", "LT", "LTE", "GT", "GTE", "AND", "OR", 
			"NOT", "LPAREN", "RPAREN", "LBRACE", "RBRACE", "LBRACK", "RBRACK", "COMMA", 
			"SEMICOLON", "COLON", "ARROW", "INTEGER", "IDENTIFIER", "COMMENT", "BLOCK_COMMENT", 
			"STRING", "ERR_CHAR", "ERR_TOKEN"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public FlowLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Flow.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\65\u0140\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31"+
		"\t\31\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t"+
		" \4!\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t"+
		"+\4,\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64"+
		"\t\64\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\4\3\4"+
		"\3\4\3\4\3\5\3\5\3\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3"+
		"\b\3\t\3\t\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\13"+
		"\3\13\3\13\3\f\3\f\3\f\3\r\3\r\3\r\3\r\3\r\3\16\3\16\3\16\3\16\3\16\3"+
		"\16\3\17\3\17\3\17\3\17\3\20\3\20\3\20\3\21\3\21\3\21\3\21\3\21\3\21\3"+
		"\21\3\22\3\22\3\22\3\22\3\23\6\23\u00bf\n\23\r\23\16\23\u00c0\3\23\3\23"+
		"\3\24\3\24\3\24\3\24\3\24\3\25\3\25\3\25\3\25\3\25\3\25\3\26\3\26\3\27"+
		"\3\27\3\30\3\30\3\31\3\31\3\32\3\32\3\32\3\32\3\33\3\33\3\33\3\34\3\34"+
		"\3\34\3\34\3\34\3\34\3\34\3\35\3\35\3\36\3\36\3\36\3\37\3\37\3 \3 \3 "+
		"\3!\3!\3!\3!\3\"\3\"\3\"\3#\3#\3#\3#\3$\3$\3%\3%\3&\3&\3\'\3\'\3(\3(\3"+
		")\3)\3*\3*\3+\3+\3,\3,\3-\3-\3-\3.\6.\u0111\n.\r.\16.\u0112\3/\3/\7/\u0117"+
		"\n/\f/\16/\u011a\13/\3\60\3\60\3\60\3\60\7\60\u0120\n\60\f\60\16\60\u0123"+
		"\13\60\3\60\3\60\3\61\3\61\7\61\u0129\n\61\f\61\16\61\u012c\13\61\3\61"+
		"\3\61\3\61\3\61\3\62\3\62\3\62\3\62\7\62\u0136\n\62\f\62\16\62\u0139\13"+
		"\62\3\62\3\62\3\63\3\63\3\64\3\64\3\u012a\2\65\3\3\5\4\7\5\t\6\13\7\r"+
		"\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21!\22#\23%\24\'\25"+
		")\26+\27-\30/\31\61\32\63\33\65\34\67\359\36;\37= ?!A\"C#E$G%I&K\'M(O"+
		")Q*S+U,W-Y.[/]\60_\61a\62c\63e\64g\65\3\2\b\5\2\13\f\17\17\"\"\3\2\62"+
		";\5\2C\\aac|\6\2\62;C\\aac|\4\2\f\f\17\17\4\2$$^^\2\u0146\2\3\3\2\2\2"+
		"\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2"+
		"\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2"+
		"\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2%\3\2\2"+
		"\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2\2\61\3\2\2"+
		"\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2\2\2;\3\2\2\2\2=\3\2"+
		"\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2\2\2I\3\2\2\2"+
		"\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\2U\3\2\2\2\2W"+
		"\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2]\3\2\2\2\2_\3\2\2\2\2a\3\2\2\2\2c\3\2"+
		"\2\2\2e\3\2\2\2\2g\3\2\2\2\3i\3\2\2\2\5p\3\2\2\2\7w\3\2\2\2\t{\3\2\2\2"+
		"\13}\3\2\2\2\r\u0083\3\2\2\2\17\u0087\3\2\2\2\21\u008b\3\2\2\2\23\u0090"+
		"\3\2\2\2\25\u0097\3\2\2\2\27\u009d\3\2\2\2\31\u00a0\3\2\2\2\33\u00a5\3"+
		"\2\2\2\35\u00ab\3\2\2\2\37\u00af\3\2\2\2!\u00b2\3\2\2\2#\u00b9\3\2\2\2"+
		"%\u00be\3\2\2\2\'\u00c4\3\2\2\2)\u00c9\3\2\2\2+\u00cf\3\2\2\2-\u00d1\3"+
		"\2\2\2/\u00d3\3\2\2\2\61\u00d5\3\2\2\2\63\u00d7\3\2\2\2\65\u00db\3\2\2"+
		"\2\67\u00de\3\2\2\29\u00e5\3\2\2\2;\u00e7\3\2\2\2=\u00ea\3\2\2\2?\u00ec"+
		"\3\2\2\2A\u00ef\3\2\2\2C\u00f3\3\2\2\2E\u00f6\3\2\2\2G\u00fa\3\2\2\2I"+
		"\u00fc\3\2\2\2K\u00fe\3\2\2\2M\u0100\3\2\2\2O\u0102\3\2\2\2Q\u0104\3\2"+
		"\2\2S\u0106\3\2\2\2U\u0108\3\2\2\2W\u010a\3\2\2\2Y\u010c\3\2\2\2[\u0110"+
		"\3\2\2\2]\u0114\3\2\2\2_\u011b\3\2\2\2a\u0126\3\2\2\2c\u0131\3\2\2\2e"+
		"\u013c\3\2\2\2g\u013e\3\2\2\2ij\7k\2\2jk\7o\2\2kl\7r\2\2lm\7q\2\2mn\7"+
		"t\2\2no\7v\2\2o\4\3\2\2\2pq\7o\2\2qr\7q\2\2rs\7f\2\2st\7w\2\2tu\7n\2\2"+
		"uv\7g\2\2v\6\3\2\2\2wx\7x\2\2xy\7c\2\2yz\7t\2\2z\b\3\2\2\2{|\7?\2\2|\n"+
		"\3\2\2\2}~\7c\2\2~\177\7t\2\2\177\u0080\7t\2\2\u0080\u0081\7c\2\2\u0081"+
		"\u0082\7{\2\2\u0082\f\3\2\2\2\u0083\u0084\7o\2\2\u0084\u0085\7c\2\2\u0085"+
		"\u0086\7r\2\2\u0086\16\3\2\2\2\u0087\u0088\7k\2\2\u0088\u0089\7p\2\2\u0089"+
		"\u008a\7v\2\2\u008a\20\3\2\2\2\u008b\u008c\7d\2\2\u008c\u008d\7q\2\2\u008d"+
		"\u008e\7q\2\2\u008e\u008f\7n\2\2\u008f\22\3\2\2\2\u0090\u0091\7u\2\2\u0091"+
		"\u0092\7v\2\2\u0092\u0093\7t\2\2\u0093\u0094\7k\2\2\u0094\u0095\7p\2\2"+
		"\u0095\u0096\7i\2\2\u0096\24\3\2\2\2\u0097\u0098\7R\2\2\u0098\u0099\7"+
		"t\2\2\u0099\u009a\7k\2\2\u009a\u009b\7p\2\2\u009b\u009c\7v\2\2\u009c\26"+
		"\3\2\2\2\u009d\u009e\7k\2\2\u009e\u009f\7h\2\2\u009f\30\3\2\2\2\u00a0"+
		"\u00a1\7g\2\2\u00a1\u00a2\7n\2\2\u00a2\u00a3\7u\2\2\u00a3\u00a4\7g\2\2"+
		"\u00a4\32\3\2\2\2\u00a5\u00a6\7y\2\2\u00a6\u00a7\7j\2\2\u00a7\u00a8\7"+
		"k\2\2\u00a8\u00a9\7n\2\2\u00a9\u00aa\7g\2\2\u00aa\34\3\2\2\2\u00ab\u00ac"+
		"\7h\2\2\u00ac\u00ad\7q\2\2\u00ad\u00ae\7t\2\2\u00ae\36\3\2\2\2\u00af\u00b0"+
		"\7k\2\2\u00b0\u00b1\7p\2\2\u00b1 \3\2\2\2\u00b2\u00b3\7t\2\2\u00b3\u00b4"+
		"\7g\2\2\u00b4\u00b5\7v\2\2\u00b5\u00b6\7w\2\2\u00b6\u00b7\7t\2\2\u00b7"+
		"\u00b8\7p\2\2\u00b8\"\3\2\2\2\u00b9\u00ba\7n\2\2\u00ba\u00bb\7g\2\2\u00bb"+
		"\u00bc\7v\2\2\u00bc$\3\2\2\2\u00bd\u00bf\t\2\2\2\u00be\u00bd\3\2\2\2\u00bf"+
		"\u00c0\3\2\2\2\u00c0\u00be\3\2\2\2\u00c0\u00c1\3\2\2\2\u00c1\u00c2\3\2"+
		"\2\2\u00c2\u00c3\b\23\2\2\u00c3&\3\2\2\2\u00c4\u00c5\7v\2\2\u00c5\u00c6"+
		"\7t\2\2\u00c6\u00c7\7w\2\2\u00c7\u00c8\7g\2\2\u00c8(\3\2\2\2\u00c9\u00ca"+
		"\7h\2\2\u00ca\u00cb\7c\2\2\u00cb\u00cc\7n\2\2\u00cc\u00cd\7u\2\2\u00cd"+
		"\u00ce\7g\2\2\u00ce*\3\2\2\2\u00cf\u00d0\7-\2\2\u00d0,\3\2\2\2\u00d1\u00d2"+
		"\7/\2\2\u00d2.\3\2\2\2\u00d3\u00d4\7,\2\2\u00d4\60\3\2\2\2\u00d5\u00d6"+
		"\7\61\2\2\u00d6\62\3\2\2\2\u00d7\u00d8\7o\2\2\u00d8\u00d9\7q\2\2\u00d9"+
		"\u00da\7f\2\2\u00da\64\3\2\2\2\u00db\u00dc\7k\2\2\u00dc\u00dd\7u\2\2\u00dd"+
		"\66\3\2\2\2\u00de\u00df\7k\2\2\u00df\u00e0\7u\2\2\u00e0\u00e1\7\"\2\2"+
		"\u00e1\u00e2\7p\2\2\u00e2\u00e3\7q\2\2\u00e3\u00e4\7v\2\2\u00e48\3\2\2"+
		"\2\u00e5\u00e6\7>\2\2\u00e6:\3\2\2\2\u00e7\u00e8\7>\2\2\u00e8\u00e9\7"+
		"?\2\2\u00e9<\3\2\2\2\u00ea\u00eb\7@\2\2\u00eb>\3\2\2\2\u00ec\u00ed\7@"+
		"\2\2\u00ed\u00ee\7?\2\2\u00ee@\3\2\2\2\u00ef\u00f0\7c\2\2\u00f0\u00f1"+
		"\7p\2\2\u00f1\u00f2\7f\2\2\u00f2B\3\2\2\2\u00f3\u00f4\7q\2\2\u00f4\u00f5"+
		"\7t\2\2\u00f5D\3\2\2\2\u00f6\u00f7\7p\2\2\u00f7\u00f8\7q\2\2\u00f8\u00f9"+
		"\7v\2\2\u00f9F\3\2\2\2\u00fa\u00fb\7*\2\2\u00fbH\3\2\2\2\u00fc\u00fd\7"+
		"+\2\2\u00fdJ\3\2\2\2\u00fe\u00ff\7}\2\2\u00ffL\3\2\2\2\u0100\u0101\7\177"+
		"\2\2\u0101N\3\2\2\2\u0102\u0103\7]\2\2\u0103P\3\2\2\2\u0104\u0105\7_\2"+
		"\2\u0105R\3\2\2\2\u0106\u0107\7.\2\2\u0107T\3\2\2\2\u0108\u0109\7=\2\2"+
		"\u0109V\3\2\2\2\u010a\u010b\7<\2\2\u010bX\3\2\2\2\u010c\u010d\7/\2\2\u010d"+
		"\u010e\7@\2\2\u010eZ\3\2\2\2\u010f\u0111\t\3\2\2\u0110\u010f\3\2\2\2\u0111"+
		"\u0112\3\2\2\2\u0112\u0110\3\2\2\2\u0112\u0113\3\2\2\2\u0113\\\3\2\2\2"+
		"\u0114\u0118\t\4\2\2\u0115\u0117\t\5\2\2\u0116\u0115\3\2\2\2\u0117\u011a"+
		"\3\2\2\2\u0118\u0116\3\2\2\2\u0118\u0119\3\2\2\2\u0119^\3\2\2\2\u011a"+
		"\u0118\3\2\2\2\u011b\u011c\7\61\2\2\u011c\u011d\7\61\2\2\u011d\u0121\3"+
		"\2\2\2\u011e\u0120\n\6\2\2\u011f\u011e\3\2\2\2\u0120\u0123\3\2\2\2\u0121"+
		"\u011f\3\2\2\2\u0121\u0122\3\2\2\2\u0122\u0124\3\2\2\2\u0123\u0121\3\2"+
		"\2\2\u0124\u0125\b\60\2\2\u0125`\3\2\2\2\u0126\u012a\7%\2\2\u0127\u0129"+
		"\13\2\2\2\u0128\u0127\3\2\2\2\u0129\u012c\3\2\2\2\u012a\u012b\3\2\2\2"+
		"\u012a\u0128\3\2\2\2\u012b\u012d\3\2\2\2\u012c\u012a\3\2\2\2\u012d\u012e"+
		"\7%\2\2\u012e\u012f\3\2\2\2\u012f\u0130\b\61\2\2\u0130b\3\2\2\2\u0131"+
		"\u0137\7$\2\2\u0132\u0136\n\7\2\2\u0133\u0134\7^\2\2\u0134\u0136\13\2"+
		"\2\2\u0135\u0132\3\2\2\2\u0135\u0133\3\2\2\2\u0136\u0139\3\2\2\2\u0137"+
		"\u0135\3\2\2\2\u0137\u0138\3\2\2\2\u0138\u013a\3\2\2\2\u0139\u0137\3\2"+
		"\2\2\u013a\u013b\7$\2\2\u013bd\3\2\2\2\u013c\u013d\13\2\2\2\u013df\3\2"+
		"\2\2\u013e\u013f\n\6\2\2\u013fh\3\2\2\2\n\2\u00c0\u0112\u0118\u0121\u012a"+
		"\u0135\u0137\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}