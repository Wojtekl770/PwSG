namespace Forms1
{
    public partial class Form1 : Form
    {
        private string currentAuthor = "You";

        public Form1()
        {
            InitializeComponent();
        }


        private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            string message = textBox1.Text;
            if (!string.IsNullOrEmpty(message))
            {
                string author = currentAuthor;
                currentAuthor = (currentAuthor == "You") ? "Not You" : "You";
                AddMessageBubble(message, author);
                textBox1.Clear();
            }
        }

        private void Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendMessage();
            }
        }



        private void AddMessageBubble(string message, string author)
        {
            // Tworzenie nowego dymka wiadomości
            MessageBubble messageBubble = new MessageBubble();
        }
    }
}
