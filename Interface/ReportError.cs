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
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            message.From = new System.Net.Mail.MailAddress("miguelpintodacosta@gmail.com");
            message.To.Add("miguelpintodacosta@gmail.com");
            message.Subject = "[beSmart] " + textBoxSubject.Text;

            try
            {
                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(textBoxAttach.Text);
                message.Attachments.Add(attach);
            }
            catch (Exception) { }

            message.Body = richTextBoxBody.Text;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("miguelpintodacosta@gmail.com", "");
            smtp.Send(message);

            MessageBox.Show("Send!");

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            sendEmail();
        }
    }
}
