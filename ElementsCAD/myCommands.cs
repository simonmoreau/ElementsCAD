// (C) Copyright 2021 by  
//
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using System.Drawing;
using Forms = System.Windows.Forms;
using System.Windows.Forms.Integration;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(ElementsCAD.MyCommands))]

namespace ElementsCAD
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class MyCommands
    {
        // The CommandMethod attribute can be applied to any public  member 
        // function of any public class.
        // The function should take no arguments and return nothing.
        // If the method is an intance member then the enclosing class is 
        // intantiated for each document. If the member is a static member then
        // the enclosing class is NOT intantiated.
        //
        // NOTE: CommandMethod has overloads where you can provide helpid and
        // context menu.

        static PaletteSet _ps = null;

        // The Command to open the Paletette Set
        [CommandMethod("MyGroup", "ElementsCAD", "ElementsCAD", CommandFlags.Modal)]
        public void ShowElementsCADPalette() // This method can have any name
        {
            // Put your command code here
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;

            if (_ps == null)

            {

                // Create the palette set
                _ps = new PaletteSet("WPF Palette");
                _ps.Size = new Size(400, 600);
                _ps.DockEnabled = (DockSides)((int)DockSides.Left + (int)DockSides.Right);

                // Create our second user control instance and
                // host it in an ElementHost, which allows
                // interop between WinForms and WPF
                MainPaletteSet mainPaletteSet = new MainPaletteSet();
                ElementHost host = new ElementHost();
                host.AutoSize = true;
                host.Dock = Forms.DockStyle.Fill;
                host.Child = mainPaletteSet;

                _ps.Add("Add ElementHost", host);

            }

            // Display our palette set

            _ps.KeepFocus = true;

            _ps.Visible = true;

            if (doc != null)
            {
                ed = doc.Editor;
                ed.WriteMessage("Hello, this open the ElementsCAD panel.");
            }
        }

        // Modal Command with localized name
        [CommandMethod("MyGroup", "MyCommand", "MyCommandLocal", CommandFlags.Modal)]
        public void MyCommand() // This method can have any name
        {
            // Put your command code here
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;
            if (doc != null)
            {
                ed = doc.Editor;
                ed.WriteMessage("Hello, this is your first command.");

            }
        }

        // Modal Command with pickfirst selection
        [CommandMethod("MyGroup", "MyPickFirst", "MyPickFirstLocal", CommandFlags.Modal | CommandFlags.UsePickSet)]
        public void MyPickFirst() // This method can have any name
        {
            PromptSelectionResult result = Application.DocumentManager.MdiActiveDocument.Editor.GetSelection();
            if (result.Status == PromptStatus.OK)
            {
                // There are selected entities
                // Put your command using pickfirst set code here
            }
            else
            {
                // There are no selected entities
                // Put your command code here
            }
        }

        // Application Session Command with localized name
        [CommandMethod("MyGroup", "MySessionCmd", "MySessionCmdLocal", CommandFlags.Modal | CommandFlags.Session)]
        public void MySessionCmd() // This method can have any name
        {
            // Put your command code here
        }

        // LispFunction is similar to CommandMethod but it creates a lisp 
        // callable function. Many return types are supported not just string
        // or integer.
        [LispFunction("MyLispFunction", "MyLispFunctionLocal")]
        public int MyLispFunction(ResultBuffer args) // This method can have any name
        {
            // Put your command code here

            // Return a value to the AutoCAD Lisp Interpreter
            return 1;
        }

    }

}
