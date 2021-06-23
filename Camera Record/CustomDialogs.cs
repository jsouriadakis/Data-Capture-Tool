using System.Windows.Forms;
using System.Drawing;

public static class Prompt
{
    private static string defaultId = "";
    private static string defaultName = "";
    private static string defaultWeight = "";
    private static string defaultReps = "";

    public static ExerciseDialogData ShowExerciseDialog()
    {
        Form prompt = new Form();
        prompt.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        prompt.Width = 300;
        prompt.Height = 240;
        prompt.Text = "Insert Exercise Data";
        Label idLabel = new Label() { Left = 30, Top = 20, Text = "Exercise Id" };
        TextBox idInputBox = new TextBox() { Left = 30, Top = 50, Width = 100 };
        idInputBox.Text = defaultId;
        Label nameLabel = new Label() { Left = 150, Top = 20, Text = "Exercise Name" };
        TextBox nameInputBox = new TextBox() { Left = 150, Top = 50, Width = 100 };
        nameInputBox.Text = defaultName;
        Label weightLabel = new Label() { Left = 30, Top = 80, Text = "Applied Weight" };
        TextBox weightInputBox = new TextBox() { Left = 30, Top = 110, Width = 100 };
        weightInputBox.Text = defaultWeight;
        Label repetitionsLabel = new Label() { Left = 150, Top = 80, Text = "Repetitions" };
        TextBox repetitionsInputBox = new TextBox() { Left = 150, Top = 110, Width = 100 };
        repetitionsInputBox.Text = defaultReps;
        Button confirmation = new Button() { Text = "Ok", Left = 30, Width = 220, Top = 160 };
        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(idLabel);
        prompt.Controls.Add(idInputBox);
        prompt.Controls.Add(nameLabel);
        prompt.Controls.Add(nameInputBox);
        prompt.Controls.Add(weightLabel);
        prompt.Controls.Add(weightInputBox);
        prompt.Controls.Add(repetitionsLabel);
        prompt.Controls.Add(repetitionsInputBox);
        prompt.ShowDialog();
        while (idInputBox.Text == "" || nameInputBox.Text == "" || weightInputBox.Text == "" || repetitionsInputBox.Text == "")
        {
            MessageBox.Show("Fill all Entries");
            prompt.ShowDialog();
            
        }
        while(!Utilities.IsDigitsOnly(idInputBox.Text) || !Utilities.IsDigitsOnly(weightInputBox.Text) || !Utilities.IsDigitsOnly(repetitionsInputBox.Text))
        {
            MessageBox.Show("Id, Weight and repetitions must be Integer Numbers");
            prompt.ShowDialog();
        }
        defaultId = idInputBox.Text;
        defaultName = nameInputBox.Text;
        defaultWeight = weightInputBox.Text;
        defaultReps = repetitionsInputBox.Text;
        ExerciseDialogData exerciseDialogData = new ExerciseDialogData(int.Parse(idInputBox.Text), nameInputBox.Text, float.Parse(weightInputBox.Text), int.Parse(repetitionsInputBox.Text));
        return exerciseDialogData;
    }

    public class ExerciseDialogData
    {
        public int id;
        public string name;
        public float weight;
        public int repetitions;

        public ExerciseDialogData(int _id, string _name, float _weight, int _repetitions)
        {
            id = _id;
            name = _name;
            weight = _weight;
            repetitions = _repetitions;
            defaultId = id.ToString();
            defaultName = name;
            defaultWeight = weight.ToString();
            defaultReps = repetitions.ToString();
        }
        
    }

}