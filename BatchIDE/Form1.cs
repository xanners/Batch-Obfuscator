﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using CodeTools;
namespace BatchIDE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private static Random random = new Random();
        public static String RandomString(int length) 
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string output;
        public string output2;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter a file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               // try
                {
                    if (checkBox2.Checked == true)
                    {
                        var input = textBox1.Text;
                        string bruh = input = string.Concat(input.Select(x => Char.IsLetterOrDigit(x) ? "%" + RandomString(6) + "%" + x : x.ToString())).TrimStart();
                        output = bruh;
                    }
                    else if (checkBox2.Checked == false)
                    {
                        output = textBox1.Text;
                    }
                    if (checkBox1.Checked == true)
                    {
                        var p1 = output + "\nDel %~0";
                        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(p1);
                        var outy = System.Convert.ToBase64String(plainTextBytes);

                        string sb = "@echo off\nCERTUTIL -f -decode \"%~f0\" \"%Temp%/test.bat\" >nul 2>&1 \ncls\n\"%Temp%/test.bat\"\nExit\n-----BEGIN CERTIFICATE-----\n" + outy + "\n-----END CERTIFICATE-----";
                        File.WriteAllText(Environment.CurrentDirectory + "/" + textBox2.Text + ".bat", sb);
                        output = sb;

                    }
                    if (checkBox3.Checked == true)
                    {
                        string outy = output + "\nDel %~0";
                        string configData1 = File.ReadAllText(Environment.CurrentDirectory + "/config.txt");
                        string configData2 = File.ReadAllText(Environment.CurrentDirectory + "/config2.txt");
                        Obfuscator o = new Obfuscator();
                        string out2 = o.Obfuscate(outy, "123");
                        string config =  $"{out2}";
                        string conjoined = configData1 + Environment.NewLine + config + configData2;

                        File.WriteAllText(Environment.CurrentDirectory + "/" + textBox2.Text + ".cs", conjoined);
                        /*/
                        Process pr = new Process();
                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = Environment.CurrentDirectory + "/csc.exe";
                        startInfo.Arguments = textBox2.Text + ".cs";
                        process.StartInfo = startInfo;
                        process.Start();
                        /*/
                        MessageBox.Show("Successfully Generated C# File. Now Making Batch File.", "Success!" + MessageBoxIcon.Information);





                    }
                    File.WriteAllText(Environment.CurrentDirectory + "/" + textBox2.Text + ".bat", output);

                    MessageBox.Show("Obfuscated Successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //catch { MessageBox.Show("Fatal Error Occured", "Error"); }
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
