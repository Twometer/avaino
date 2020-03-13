using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace avaino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Scintilla.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // Configure the default style
            Scintilla.StyleResetDefault();
            Scintilla.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            Scintilla.Styles[ScintillaNET.Style.Default].Size = 11;
            Scintilla.Styles[ScintillaNET.Style.Default].BackColor = IntToColor(0x282C34);
            Scintilla.Styles[ScintillaNET.Style.Default].ForeColor = IntToColor(0xabb2bf);
            Scintilla.StyleClearAll();

            // Configure the margins
            Scintilla.Margins[0].Type = MarginType.Number;
            Scintilla.Margins[0].Width = 32;
            Scintilla.Margins[0].BackColor = IntToColor(0x212121);
            Scintilla.Styles[ScintillaNET.Style.LineNumber].ForeColor = IntToColor(0x495161);
            Scintilla.Styles[ScintillaNET.Style.LineNumber].BackColor = IntToColor(0x282C34);
            Scintilla.CaretForeColor = Color.FromArgb(255,255,255,255);
            var c = IntToColor(0x3E4451);
            Scintilla.SetSelectionBackColor(true, Color.FromArgb(c.A,c.R,c.G,c.B));
            Scintilla.SelectionEolFilled = true;
            Scintilla.IndentationGuides = IndentView.LookBoth;
            // Configure the CPP (C#) lexer styles
            Scintilla.Styles[ScintillaNET.Style.Cpp.Identifier].ForeColor = IntToColor(0xabb2bf);

            Scintilla.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = IntToColor(0x5c6370);
            Scintilla.Styles[ScintillaNET.Style.Cpp.Comment].Italic = true;
            Scintilla.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = IntToColor(0x5c6370);
            Scintilla.Styles[ScintillaNET.Style.Cpp.CommentLine].Italic = true;
            Scintilla.Styles[ScintillaNET.Style.Cpp.CommentDoc].ForeColor = IntToColor(0x5c6370);
            Scintilla.Styles[ScintillaNET.Style.Cpp.CommentDoc].Italic = true;
            Scintilla.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x5c6370);
            Scintilla.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].Italic = true;

            Scintilla.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = IntToColor(0xd19a66);
            Scintilla.Styles[ScintillaNET.Style.Cpp.String].ForeColor = IntToColor(0x98C379);
            Scintilla.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = IntToColor(0x98C379);

            Scintilla.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = IntToColor(0xe06c75);
            Scintilla.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = IntToColor(0xc678dd);
            Scintilla.Styles[ScintillaNET.Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            Scintilla.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = IntToColor(0x61afef);     // keywords
            Scintilla.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = IntToColor(0xd19a66);    // classes n stuff

            Scintilla.Styles[ScintillaNET.Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            Scintilla.Styles[ScintillaNET.Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            Scintilla.Styles[ScintillaNET.Style.Cpp.GlobalClass].ForeColor = IntToColor(0x56b6c2);

            Scintilla.Lexer = Lexer.Cpp;

            Scintilla.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            Scintilla.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET ICACHE_RAM_ATTR Serial");
        }

        public static System.Drawing.Color IntToColor(int rgb)
        {
            return System.Drawing.Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }
    }
}
