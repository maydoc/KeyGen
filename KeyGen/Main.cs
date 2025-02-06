using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGen {
    public partial class Main : Form {
        public Main() {
            InitializeComponent();
            this.Paginas_Controle.Selected += Paginas_Controle_Selected;
            this.FormClosing += Main_FormClosing;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason != CloseReason.ApplicationExitCall) {
                if (gerando) {
                    switch (MessageBox.Show("Ainda há tarefas em execução, deseja realmente sair do programa?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) {
                        case DialogResult.Yes:
                            cancel = true;
                            Application.Exit();
                            break;
                        case DialogResult.No:
                            e.Cancel = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void Paginas_Controle_Selected(object sender, TabControlEventArgs e) {
            if (e.TabPage == Page_Multi_Strings) {
                while (this.Height < 293) {
                    this.Height += 5;
                }

            }
            else {
                while (this.Height > 268) {
                    this.Height -= 5;
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void BT_Gerar_Click(object sender, EventArgs e) {
            if (Separador.Text == string.Empty && CB_Seperador.Checked) {
                MessageBox.Show("Separador não pode ser nulo!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Lenght_Chave.Value % Casas_Chave.Value != 0) {
                Casas_Chave.ForeColor = Color.Red;
                label2.ForeColor = Color.Red;
                MessageBox.Show("Valor divergente entre o comprimento da chave e a quantidade por casas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Casas_Chave.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            var chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (!CB_Numeros.Checked) {
                chars = chars.Replace("0123456789", "");
            }
            if (!CB_Letras_Minusculas.Checked) {
                chars = chars.Replace("abcdefghijklmnopqrstuvwxyz", "");
            }
            if (!CB_Letras_Maiusculas.Checked) {
                chars = chars.Replace("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "");
            }
            if (chars.Length == 0) {
                MessageBox.Show("Selecione pelo menos um tipo de caractere para gerar a chave!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string keycrua = randomtext((int) Lenght_Chave.Value, chars);
            string keycasada = string.Empty;
            string temp = string.Empty;
            foreach (var caract in keycrua) {
                temp += caract;
                if (temp.Length == (int) Casas_Chave.Value) {
                    if (keycasada.Length == 0) {
                        keycasada = temp;
                    }
                    else {
                        if (CB_Seperador.Checked) keycasada += Separador.Text + temp;
                        else keycasada += temp;
                    }
                    temp = string.Empty;
                }
            }
            TB_Chave_Gerada.Text = keycasada;
            tamanho_chave.Text = "TM: " + keycasada.Length.ToString();
            if (CB_Seperador.Checked) { tamanho_chave_sem_separador.Text = "TM S/S: " + keycasada.Replace(Separador.Text, "").Length.ToString(); } else { tamanho_chave_sem_separador.Text = "TM S/S: " + keycasada.Length.ToString(); }

            if (CB_Clipboard.Checked) {
                Clipboard.SetText(keycasada);
            }
        }
        public static string randomtext(int tamanho, string string_char) {
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(string_char, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        private void BT_Clipboard_Click(object sender, EventArgs e) {
            if (TB_Chave_Gerada.TextLength != 0) {
                Clipboard.SetText(TB_Chave_Gerada.Text);
            }
        }

        private void Page_Multi_Strings_Click(object sender, EventArgs e) {

        }
        bool gerando = false;
        bool cancel = false;
        private async void BT_Gerar_MT_Click(object sender, EventArgs e) {
            if (Separador_MT.Text == string.Empty && CB_Seperador_MT.Checked) {
                MessageBox.Show("Separador não pode ser nulo!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Lenght_Chave_MT.Value % Casas_Chave_MT.Value != 0) {
                Casas_Chave_MT.ForeColor = Color.Red;
                label7.ForeColor = Color.Red;
                MessageBox.Show("Valor divergente entre o comprimento da chave e a quantidade por casas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Casas_Chave_MT.ForeColor = Color.Black;
            label7.ForeColor = Color.Black;
            var chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (!CB_Numeros_MT.Checked) {
                chars = chars.Replace("0123456789", "");
            }
            if (!CB_Letras_Minusculas_MT.Checked) {
                chars = chars.Replace("abcdefghijklmnopqrstuvwxyz", "");
            }
            if (!CB_Letras_Maiusculas_MT.Checked) {
                chars = chars.Replace("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "");
            }
            if (chars.Length == 0) {
                MessageBox.Show("Selecione pelo menos um tipo de caractere para gerar a chave!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BT_Gerar_MT.Enabled = false;
            BT_Salvar_Cancelar.Image = Properties.Resources.Cancelar;
            Tip_Ajuda.SetToolTip(BT_Salvar_Cancelar, "Cancelar operação atual");
            gerando = true;
            Quantidade_key.Enabled = false;
            Lenght_Chave_MT.Enabled = false;
            CB_Clipboard_MT.Enabled = false;
            CB_Letras_Maiusculas_MT.Enabled = false;
            CB_Letras_Minusculas_MT.Enabled = false;
            CB_Numeros_MT.Enabled = false;
            CB_Seperador_MT.Enabled = false;
            Separador_MT.Enabled = false;
            Casas_Chave_MT.Enabled = false;
            BT_Clipboard_MT.Enabled = false;
            LB_Porcentagem.Text = "0%";
            string keycasada = string.Empty;
            Lista_Keys.Items.Clear();
            Barra_Progresso.Maximum = (int) Quantidade_key.Value;
            for (int i = 0; i < Quantidade_key.Value; i++) {
                Barra_Progresso.Value++;
                LB_Porcentagem.Text = ((100 / Quantidade_key.Value) * i).ToString("F2") + "%";
                if (cancel) {
                    BT_Gerar_MT.Enabled = true;
                    Quantidade_key.Enabled = true;
                    Lenght_Chave_MT.Enabled = true;
                    CB_Clipboard_MT.Enabled = true;
                    CB_Letras_Maiusculas_MT.Enabled = true;
                    CB_Letras_Minusculas_MT.Enabled = true;
                    CB_Numeros_MT.Enabled = true;
                    CB_Seperador_MT.Enabled = true;
                    if (CB_Seperador_MT.Checked) Separador_MT.Enabled = true;
                    Casas_Chave_MT.Enabled = true;
                    BT_Salvar_Cancelar.Image = Properties.Resources.Salvar;
                    Tip_Ajuda.SetToolTip(BT_Salvar_Cancelar, "Salvar códigos gerados");
                    MessageBox.Show("Geração de chaves cancelada com êxito!", "Êxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LB_Porcentagem.Text = "0%";
                    Barra_Progresso.Value = 0;
                    cancel = false;
                    gerando = false;
                    return;
                }
                string keycrua = randomtext((int) Lenght_Chave_MT.Value, chars);
                keycasada = string.Empty;
                string temp = string.Empty;
                foreach (var caract in keycrua) {
                    temp += caract;
                    if (temp.Length == (int) Casas_Chave_MT.Value) {
                        if (keycasada.Length == 0) {
                            keycasada = temp;
                        }
                        else {
                            if (CB_Seperador_MT.Checked) keycasada += Separador_MT.Text + temp;
                            else keycasada += temp;
                        }
                        temp = string.Empty;
                    }
                }
                Lista_Keys.Items.Add(keycasada);
                Quantidade_Lista.Text = "Qnt: " + Lista_Keys.Items.Count;
                Lista_Keys.SelectedIndex = Lista_Keys.Items.Count - 1;
                tamanho_chave_MT.Text = "TM: " + keycasada.Length.ToString();
                if (CB_Seperador_MT.Checked) { tamanho_chave_sem_separador_MT.Text = "TM S/S: " + keycasada.Replace(Separador_MT.Text, "").Length.ToString(); } else { tamanho_chave_sem_separador_MT.Text = "TM S/S: " + keycasada.Length.ToString(); }
                await Task.Delay(10);
            }
            BT_Gerar_MT.Enabled = true;
            Quantidade_key.Enabled = true;
            Lenght_Chave_MT.Enabled = true;
            CB_Clipboard_MT.Enabled = true;
            CB_Letras_Maiusculas_MT.Enabled = true;
            CB_Letras_Minusculas_MT.Enabled = true;
            CB_Numeros_MT.Enabled = true;
            CB_Seperador_MT.Enabled = true;
            BT_Clipboard_MT.Enabled = true;
            if (CB_Seperador_MT.Checked) Separador_MT.Enabled = true;
            Casas_Chave_MT.Enabled = true;
            LB_Porcentagem.Text = "100%";
            BT_Salvar_Cancelar.Image = Properties.Resources.Salvar;
            Tip_Ajuda.SetToolTip(BT_Salvar_Cancelar, "Salvar códigos gerados");
            MessageBox.Show("Concluído!", "Êxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gerando = false;
            LB_Porcentagem.Text = "0%";
            Barra_Progresso.Value = 0;
            if (CB_Clipboard_MT.Checked) {
                Clipboard.SetText(keycasada);
            }
        }

        private void BT_Clipboard_MT_Click(object sender, EventArgs e) {
            if (Lista_Keys.SelectedItem != null) {
                Clipboard.SetText(Lista_Keys.SelectedItem.ToString());
            }
        }

        private void CB_Seperador_MT_CheckedChanged(object sender, EventArgs e) {
            if (CB_Seperador_MT.Checked) {
                Separador_MT.Enabled = true;
            }
            else {
                Separador_MT.Enabled = false;
            }
        }

        private void CB_Seperador_CheckedChanged(object sender, EventArgs e) {
            if (CB_Seperador.Checked) {
                Separador.Enabled = true;
            }
            else {
                Separador.Enabled = false;
            }
        }

        private void BT_Salvar_Cancelar_Click(object sender, EventArgs e) {
            if (gerando) {
                cancel = true;
            }
            else {
                if (Lista_Keys.Items.Count > 0) {
                    if (Salvar_Lista.ShowDialog() == DialogResult.OK) {
                        System.IO.File.Create(Salvar_Lista.FileName).Close();
                        System.IO.TextWriter file = System.IO.File.AppendText(Salvar_Lista.FileName);
                        foreach (var item in Lista_Keys.Items) {
                            file.WriteLine(item.ToString());
                        }
                        file.Close();
                        Process.Start("explorer.exe", @"\select, " + Salvar_Lista.FileName);
                        MessageBox.Show("Lista salva com sucesso em: " + Salvar_Lista.FileName, "Salvar lista", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Salvar_Lista.FileName = "Lista_Keys.txt";
                    }
                }
                else {
                    MessageBox.Show("Nenhum código gerado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void Casas_Chave_MT_ValueChanged(object sender, EventArgs e) {
            if (Lenght_Chave_MT.Value % Casas_Chave_MT.Value != 0) {
                Icon_Aviso.Visible = true;
                Casas_Chave_MT.Width = 89;
                Casas_Chave_MT.ForeColor = Color.Red;
                label7.ForeColor = Color.Red;
                Tip_Ajuda.SetToolTip(Icon_Aviso, "Valor divergente com o comprimento da chave\nObs: O comprimento da chave deve ser divisível pela quantidade de casa\nValores recomendados: ");
                int cont = 0;
                int div = 2;
                while (true) {
                    if (div > Lenght_Chave_MT.Value) {
                        break;
                    }
                    if (Lenght_Chave_MT.Value % div == 0) {
                        if (cont != 0) Tip_Ajuda.SetToolTip(Icon_Aviso, Tip_Ajuda.GetToolTip(Icon_Aviso) + $", {div}");
                        else Tip_Ajuda.SetToolTip(Icon_Aviso, Tip_Ajuda.GetToolTip(Icon_Aviso) + $"{div}");
                        cont++;
                        div++;
                    }
                    div++;
                }
            }
            else {
                Icon_Aviso.Visible = false; Casas_Chave_MT.Width = 116; Tip_Ajuda.SetToolTip(Icon_Aviso, "Valor divergente com o comprimento da chave\nObs: O comprimento da chave deve ser divisível pela quantidade de casa");
                Casas_Chave_MT.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
            }
        }

        private async void Casas_Chave_ValueChanged(object sender, EventArgs e) {
            if (Lenght_Chave.Value % Casas_Chave.Value != 0) {
                Icon_Aviso_U.Visible = true;
                Casas_Chave.Width = 89;
                Tip_Ajuda.SetToolTip(Icon_Aviso_U, "Valor divergente com o comprimento da chave\nObs: O comprimento da chave deve ser divisível pela quantidade de casa\nValores recomendados: ");
                Casas_Chave.ForeColor = Color.Red;
                label2.ForeColor = Color.Red;
                int cont = 0;
                int div = 2;
                while (true) {
                    if (div > Lenght_Chave.Value) {
                        break;
                    }
                    if (Lenght_Chave.Value % div == 0) {
                        if (cont != 0) Tip_Ajuda.SetToolTip(Icon_Aviso_U, Tip_Ajuda.GetToolTip(Icon_Aviso_U) + $", {div}");
                        else Tip_Ajuda.SetToolTip(Icon_Aviso_U, Tip_Ajuda.GetToolTip(Icon_Aviso_U) + $"{div}");
                        cont++;
                        div++;
                    }
                    div++;
                }
            }
            else {
                Icon_Aviso_U.Visible = false; Casas_Chave.Width = 116; Tip_Ajuda.SetToolTip(Icon_Aviso_U, "Valor divergente com o comprimento da chave\nObs: O comprimento da chave deve ser divisível pela quantidade de casa");
                Casas_Chave.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
            }
        }
    }
}
