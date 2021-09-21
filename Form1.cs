using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab04_Application.Model;
using Lab04_Application.IO;

namespace Lab04_Application
{
    public partial class Form1 : Form
    {
        List<Student> students;
        private IDataSource dataSource;
        private string filename;
        public Form1(IDataSource dataSource, string filename)
        {
            students = new List<Student>();
            this.dataSource = dataSource;
            students = dataSource.GetData(filename);
            this.filename = filename;
            InitializeComponent();
        }
        

        private void btn_exit_Click(object sender, EventArgs e)
        {
            /*List<Student> tocheck = dataSource.GetData(filename);
            if (tocheck == students)
            {
                Application.Exit();
            }
            if (tocheck != students)
            {
                DialogResult dlg = MessageBox.Show("Lưu Những Thay Đổi?", "Thoát", MessageBoxButtons.YesNoCancel);
                if (dlg == DialogResult.No)
                {
                    Application.Exit();
                }
                if (dlg == DialogResult.Cancel)
                    return;
                if (dlg == DialogResult.Yes)
                {
                    dataSource.SaveChange(students, filename);
                    Application.Exit();
                }
            }*/
            Application.Exit();
        }

        private void btn_Default_Click(object sender, EventArgs e)
        {
            ResetControlls();
        }
        private void ResetControlls()
        {
            pb_avatar.ImageLocation = "";
            msktxt_id.Text = "";
            txt_name.Text = "";
            txt_email.Text = "";
            txt_address.Text = "";
            dtp_birthday.Value = DateTime.Now;
            msktxt_phoneNumber.Text = "";
            rd_male.Checked = true;
            cbb_class.Text = "";
            txt_path.Text = "";
        }
        private Student GetStudent()
        {
            Student sd = new Student();
            sd.id = msktxt_id.Text;
            sd.name = txt_name.Text;
            sd.email = txt_email.Text;
            sd.address = txt_address.Text;
            sd.path = txt_path.Text;
            sd.birthday = dtp_birthday.Value;
            sd.sex = rd_male.Checked == true ? true : false;
            sd.classroom = cbb_class.Text;
            sd.phone = msktxt_phoneNumber.Text;
            return sd;
        }
        private void LoadStudentToControlls(Student student)
        {
            msktxt_id.Text = student.id;
            txt_name.Text = student.name;
            txt_email.Text = student.email;
            txt_address.Text = student.address;
            txt_path.Text = student.path;
            msktxt_phoneNumber.Text = student.phone;
            pb_avatar.ImageLocation=(student.path);
            rd_male.Checked = true;
            rd_FeMale.Checked = student.sex == true ? false : true;
            cbb_class.Text = student.classroom;
            dtp_birthday.Value = student.birthday;
        }
        private void LoadStudentToListView(Student student)
        {
            ListViewItem item = new ListViewItem(student.id);
            item.SubItems.Add(student.name);
            item.SubItems.Add(student.sex==true?"Nam":"Nữ");
            item.SubItems.Add(student.birthday.ToShortDateString());
            item.SubItems.Add(student.classroom);
            item.SubItems.Add(student.phone);
            item.SubItems.Add(student.email);
            item.SubItems.Add(student.address);
            item.SubItems.Add(student.path);
            ls_students.Items.Add(item);
        }
        private Student GetStudentFormListView(ListViewItem item)
        {
            Student sd = new Student();
            sd.id = item.SubItems[0].Text;
            sd.name = item.SubItems[1].Text;
            sd.email = item.SubItems[6].Text;
            sd.address = item.SubItems[7].Text;
            sd.path = item.SubItems[8].Text;
            sd.birthday = DateTime.Parse(item.SubItems[3].Text);
            sd.sex = item.SubItems[2].Text == "Nam" ? true : false;
            sd.classroom = item.SubItems[4].Text;
            sd.phone = item.SubItems[5].Text;
            return sd;
        }
        private void LoadStudentsToListView(List<Student> students)
        {
            ls_students.Items.Clear();
            foreach (Student sd in students)
                LoadStudentToListView(sd);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Student student = students.Find(x => x.id == msktxt_id.Text);
            if (student != null)
            {
                for(int i = 0; i < students.Count; i++)
                {
                    if (students[i].id == msktxt_id.Text)
                    {
                        students[i] = GetStudent();
                        LoadStudentsToListView(students);
                        dataSource.SaveChange(students, filename);
                        return;
                    }
                }
            }
            else
            {
                if (msktxt_id.Text == "")
                {
                    MessageBox.Show("Vui Lòng Điền Đủ Thông Tin", "Lỗi Thêm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    students.Add(GetStudent());
                    LoadStudentToListView(GetStudent());
                }
                
            }
            dataSource.SaveChange(students, filename);
        }

        private void btn_choseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Picture";
            dlg.Filter = "Image Files (JPEG, GIF, BMP, etc.)|"
                        + "*.jpg;*.jpeg;*.gif;*.bmp;"
                        + "*.tif;*.tiff;*.png|"
                        + "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
                        + "GIF files (*.gif)|*.gif|"
                        + "BMP files (*.bmp)|*.bmp|"
                        + "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
                        + "PNG files (*.png)|*.png|"
                        + "All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.CurrentDirectory;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var filename = dlg.FileName;
                txt_path.Text = filename;
                pb_avatar.Load(filename);
            }
        }

        private void ls_students_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ls_students.SelectedItems.Count > 0)
            {
                ListViewItem selecteditem = ls_students.SelectedItems[0];
                LoadStudentToControlls(GetStudentFormListView(selecteditem));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudentsToListView(students);
        }

        private void reload_Click(object sender, EventArgs e)
        {
            students = dataSource.GetData(filename);
            LoadStudentsToListView(students);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (ls_students.CheckedItems.Count > 0)
            {
                for(int i = 0; i < ls_students.CheckedItems.Count; i++)
                {
                    for(int y=0;y<students.Count;y++)
                    {
                        if (ls_students.CheckedItems[i].SubItems[0].Text == students[y].id)
                        {
                            students.RemoveAt(y);
                        }
                    }
                }
                LoadStudentsToListView(students);
            }
        }
      
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
                DialogResult dlg = MessageBox.Show("Lưu Những Thay Đổi?", "Thoát", MessageBoxButtons.YesNo);
                if (dlg == DialogResult.No)
                {
                    Application.Exit();
                }
                if (dlg == DialogResult.Yes)
                {
                    dataSource.SaveChange(students, filename);
                    Application.Exit();
                }
        }
    }
}
