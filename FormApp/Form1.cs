using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApp
{
    public partial class richTextBox1 : Form
    {
        private int Sayac { get; set; } = 0;
        
        public richTextBox1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbxSayaç.Text = Sayac++.ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = await ReadFileAsync();
        }

        private string ReadFile()
        {
            string data = string.Empty;
            
                using (StreamReader streamReader = new StreamReader("dosya.txt"))
                {
                    Thread.Sleep(5000);
                    data = streamReader.ReadToEnd();
                }
            
           
           
            return data;
        }
        //Asenkron metodlar geriye birşey döndürmeyecekse geri dönüş tipi Task olacak bu senkron metodlardaki void e karşılık gelir.

        private async Task<string> ReadFileAsync() {

            string data = string.Empty;
            using(StreamReader s=new StreamReader("dosya.txt"))
            {
                //her Asenkron metod  farklı bir thread kullanmaz
                Task<string> mytask =  s.ReadToEndAsync();//okuma işlemiyle ilgili ek bir thread yok ,ıo driver bu işi yapar.
                /*burada farklı bir işlem yaptığımızı farz edelim ve bu işlem 10 saniye sürsün
                  ReadToAsync metodu ise 2 saniye sürsün burada bir sorun olmaz data değişkenine veri yüklenir
                ancak ReadToAsync metodu 15 saniye sürer buradaki işlem yine 10 saniye sürerse await keywordünün olduğu
                yerde dönecek olan veri 5 ssaniye beklenmiş olur ancak ana thread yine de bloklanmaz.
                 */
                 await Task.Delay(5000);
                data = await mytask;

                return data;
            }
            
        
        }
    }
}
