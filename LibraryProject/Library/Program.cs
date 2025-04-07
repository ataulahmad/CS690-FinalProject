namespace Library;

class Program {

    static void Main(string[] args) {
        if (args.Length > 0) {
            string command = args[0].ToLower();
            if (command == "notify") {
                Notifier notifier = new Notifier();
                notifier.CheckAndNotify();
            }
        } else {
            ConsoleUI ui = new ConsoleUI();
            ui.LoginMenu();
        }
    }

}
