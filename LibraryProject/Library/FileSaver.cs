namespace Library;

using System.IO;

public class FileSaver {
    string fileName;

    public FileSaver(string fileName) {
        this.fileName = fileName;
        File.AppendText(fileName).Close();
    }

    public void AppendLine(string line) {
        File.AppendAllText(this.fileName, line + Environment.NewLine);
    }

    public string[] GetAllLines() {
        return File.ReadAllLines(this.fileName);
    }

    public void EmptyFile() {
        File.Create(this.fileName).Close();
    }
}