﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace NYSS
{
    public partial class TextEncoder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Encrypt_Click(object sender, EventArgs e)
        {
            ErrorsRefreshed();
            string s = TextInput.Text;
            string key = Key.Text;
            if (!Cryptographer.KeyValidator(key) || key == "")
            {
                Error.Text = "Введите корректный ключ";
            }
            else if (s != "")
            {
                EncryptedText.Text = Cryptographer.EncryptText(s, key);

            }
            else
            {
                Error.Text = "Введите текст";
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            ErrorsRefreshed();
            try
            {
                if (Validator.FileNameValidator(FileName.Text))
                {
                    if (Directory.Text != "")
                    {
                        File.WriteAllText(Validator.PathValidator(Directory.Text) + FileName.Text + ".txt", EncryptedText.Text);
                        SaveError.Text = "Сохранено!";
                    }
                    else
                    {
                        SaveError.Text = "Введите директорию для сохранения";
                    }
                }
                else
                {
                    FileNameError.Text = "Недопустимое имя файла";
                }


            }
            catch (Exception ex)
            {
                SaveError.Text = ex.Message;
            }
        }

        protected void Download_Click(object sender, EventArgs e)
        {
            ErrorsRefreshed();
            try
            {
                if (Validator.FileNameValidator(FileName.Text))
                {
                    File.WriteAllText(Server.MapPath("~/") + "TXTFile.txt", EncryptedText.Text);
                    Response.ContentType = "text/plain";
                    Response.AppendHeader("Content-Disposition", $"attachment; filename={FileName.Text}.txt");
                    Response.TransmitFile(Server.MapPath("~/") + "TXTFile.txt");
                    Response.End();

                }
                else
                {
                    FileNameError.Text = "Недопустимое имя файла";
                }
            }
            catch (Exception ex)
            {
                DownloadError.Text = ex.Message;
            }
        }
        void ErrorsRefreshed()
        {

            Error.Text = "";
            SaveError.Text = "";
            FileNameError.Text = "";
            DownloadError.Text = "";

        }

    }
}