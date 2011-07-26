using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public partial class ReportError : Form
    {
        public ReportError()
        {
            InitializeComponent();
        }

        private void sendEmail()
        {
            if (textBoxFrom.Text.Equals(""))
            {
                MessageBox.Show("From empty!");
                return;
            }

            buttonSend.Text = "Sending...";
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            message.From = new System.Net.Mail.MailAddress("miguelpintodacosta@gmail.com");
            message.To.Add("miguelpintodacosta@gmail.com");
            //message.To.Add("anaisamp@gmail.com");
            //message.To.Add("tiagoalvesabreu@gmail.com");
            //message.To.Add("emanspace@gmail.com");
            message.To.Add("besmart.software@gmail.com");

            message.Subject = "[beSmart] Report Error/Suggestion";

            try
            {
                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(textBoxAttach.Text);
                message.Attachments.Add(attach);
            }
            catch (Exception) { }

            string body = "FROM: " + textBoxFrom.Text;
            body += "SUBJECT: " + textBoxSubject.Text;
            body += "\n\n_____________________________________________________\n\n";
            body += richTextBoxBody.Text;
            message.Body = body;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("besmart.software@gmail.com", "li4grupo13");

            try
            {
                smtp.Timeout = 15;
                smtp.Send(message);
                this.Cursor = System.Windows.Forms.Cursors.Default;
                MessageBox.Show("Send!");

            }
            catch (Exception e)
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                MessageBox.Show(e.Message);
            }

            this.Cursor = System.Windows.Forms.Cursors.Default;
            buttonSend.Text = "Send";

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            sendEmail();
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();
            textBoxAttach.Text = o.FileName;
        }
    }
}
