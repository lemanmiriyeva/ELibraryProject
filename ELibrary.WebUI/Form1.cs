using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ELibrary.Business.Abstract;
using ELibrary.Business.DependencyResolvers.Ninject;
using ELibrary.Entities.Concrete;
using ELibrary.WebUI.Helpers;

namespace ELibrary.WebUI
{
    public partial class Form1 : Form
    {
        private readonly IBookService _bookService;
        private readonly IEmployeeService _employeeService;
        readonly Utilities _utilities = new Utilities();
        public Form1()
        {
            _employeeService = InstanceFactory.GetInstance<IEmployeeService>();
            _bookService = InstanceFactory.GetInstance<IBookService>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadBooks();
            LoadEmployees();
        }
        private void LoadBooks()
        {
            dgwBooks.DataSource = _bookService.GetAll().ToList();
        }
        private void LoadEmployees()
        {
            dgwEmployees.DataSource = _employeeService.GetAll().ToList();
        }

        private void dgwBooks_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            var text = dgwBooks.CurrentRow.Cells;
            tbxTitleUpdate.Text = text[1].Value.ToString();
            tbxAuthorUpdate.Text = text[2].Value.ToString();
            tbxCountUpdate.Text = text[3].Value.ToString();
            tbxCategoryUpdate.Text = text[4].Value.ToString();
            tbxRentalpriceUpdate.Text = text[5].Value.ToString();
            tbxLanguageUpdate.Text = text[6].Value.ToString();
            tbxSizeUpdate.Text = text[7].Value.ToString();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                _bookService.Add(new Book
                {
                    BookTitle = tbxTitleAdd.Text,
                    Author = tbxAuthorAdd.Text,
                    BookCount = Convert.ToInt32(tbxCountAdd.Text),
                    Category = tbxCategoryAdd.Text,
                    Language = tbxLanguageAdd.Text,
                    RentalPrice = Convert.ToDecimal(tbxPriceAdd.Text),
                    Size = Convert.ToInt32(tbxSizeAdd.Text)
                });
                MessageBox.Show("Added!", "Message");
                LoadBooks();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            _bookService.Update(new Book
            {
                Id = Convert.ToInt32(dgwBooks.CurrentRow.Cells[0].Value),
                BookTitle = tbxTitleUpdate.Text,
                Author = tbxAuthorUpdate.Text,
                BookCount = Convert.ToInt32(tbxCountUpdate.Text),
                Category = tbxCategoryUpdate.Text,
                Language = tbxLanguageUpdate.Text,
                RentalPrice = Convert.ToDecimal(tbxRentalpriceUpdate.Text),
                Size = Convert.ToInt32(tbxSizeUpdate.Text)
            });
            MessageBox.Show("Updated!", "Message");
            LoadBooks();
        }

        private void tbxIdSearch_TextChanged_1(object sender, EventArgs e)
        {
            var id = tbxIdSearch.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, id, () => { List<Book> results = new List<Book>(); results.Add(_bookService.GetById(Convert.ToInt32(id))); dgwBooks.DataSource = results; }, LoadBooks);

        }

        private void tbxTitleSearch_TextChanged(object sender, EventArgs e)
        {
            var text = tbxTitleSearch.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, text, () => { dgwBooks.DataSource = _bookService.GetByName(text); }, LoadBooks);
        }

        private void tbxAuthorSearch_TextChanged_1(object sender, EventArgs e)
        {
            var author = tbxAuthorSearch.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, author, () => { dgwBooks.DataSource = _bookService.SearchByAuthor(author); }, LoadBooks);

        }

        private void tbxCountSearch_TextChanged_1(object sender, EventArgs e)
        {
            var count = tbxCountSearch.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, count, () => { dgwBooks.DataSource = _bookService.GetByCount(Convert.ToInt32(count)); }, LoadBooks);

        }

        private void tbxCategorySearch_TextChanged_1(object sender, EventArgs e)
        {
            var category = tbxCategorySearch.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, category, () => { dgwBooks.DataSource = _bookService.GetByCategory(category); }, LoadBooks);

        }

        private void tbxLanguageSearch_TextChanged_1(object sender, EventArgs e)
        {
            var language = tbxLanguageSearch.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, language, () => { dgwBooks.DataSource = _bookService.GetByLanguage(language); }, LoadBooks);

        }
        private void LoadBySize()
        {
            var minSize = tbxMin.Text;
            int min = 0;
            int max = 1000;
            _utilities.Check(_utilities.IsNullOrEmpty, minSize, () => { min = Convert.ToInt32(minSize); });
            var maxSize = tbxMax.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, maxSize, () => { max = Convert.ToInt32(maxSize); });
            dgwBooks.DataSource = _bookService.GetBySize(min, max);
        }

        private void LoadByPrice()
        {
            var minAge = tbxPriceMin.Text;
            decimal min = 0;
            decimal max = 100;
            _utilities.Check(_utilities.IsNullOrEmpty, minAge, () => { min = Convert.ToDecimal(minAge); });

            var maxAge = tbxPriceMax.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, maxAge, () => { max = Convert.ToDecimal(maxAge); });
            dgwBooks.DataSource = _bookService.GetByPrice(min, max);
        }

        private void tbxMin_TextChanged_1(object sender, EventArgs e)
        {
            _utilities.Check(_utilities.IsNullOrEmpty, tbxMin.Text, LoadBySize, LoadBooks);
        }

        private void tbxMax_TextChanged_1(object sender, EventArgs e)
        {
            _utilities.Check(_utilities.IsNullOrEmpty, tbxMax.Text, LoadBySize, LoadBooks);
        }

        private void tbxPriceMin_TextChanged(object sender, EventArgs e)
        {
            _utilities.Check(_utilities.IsNullOrEmpty, tbxPriceMin.Text, LoadByPrice, LoadBooks);
        }

        private void tbxPriceMax_TextChanged_1(object sender, EventArgs e)
        {
            _utilities.Check(_utilities.IsNullOrEmpty, tbxPriceMax.Text, LoadByPrice, LoadBooks);
        }
        private void LoadByAge()
        {
            var minAge = tbxMinAgeEmployee.Text;
            int min = 18;
            int max = 65;
            _utilities.Check(_utilities.IsNullOrEmpty, minAge, () => { min = Convert.ToInt32(minAge); });
            var maxAge = tbxMaxAgeEmployee.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, maxAge, () => { max = Convert.ToInt32(maxAge); });
            dgwEmployees.DataSource = _employeeService.GetByAge(min, max);
        }
        private void LoadBySalary()
        {
            var minSalary = tbxMinSalaryEmployee.Text;
            decimal min = 0;
            decimal max = 1000;
            _utilities.Check(_utilities.IsNullOrEmpty, minSalary, () => { min = Convert.ToDecimal(minSalary); });

            var maxSalary = tbxMaxSalaryEmployee.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, maxSalary, () => { max = Convert.ToDecimal(maxSalary); });
            dgwEmployees.DataSource = _employeeService.GetBySalary(min, max);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _bookService.Delete(new Book
            {
                Id = Convert.ToInt32(dgwBooks.CurrentRow.Cells[0].Value),

            });
            MessageBox.Show("Deleted!", "Message");
            LoadBooks();
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            _employeeService.Delete(new Employee
            {
                Id = Convert.ToInt32(dgwEmployees.CurrentRow.Cells[0].Value),

            });
            MessageBox.Show("Deleted!", "Message");
            LoadEmployees();
        }

        private void btnEmployeeAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                _employeeService.Add(new Employee
                {
                    FirstName = tbxEmployeeFNameAdd.Text,
                    LastName = tbxLNameAddEmployee.Text,
                    Age = Convert.ToInt32(tbxAgeAddEmployee.Text),
                    Position = tbxPositionAdd.Text,
                    Salary = Convert.ToDecimal(tbxSalaryAdd.Text)
                });
                MessageBox.Show("Added!", "Message");
                LoadEmployees();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }

        }

        private void btnEmployeeUpdate_Click_1(object sender, EventArgs e)
        {

            _employeeService.Update(new Employee
            {
                Id = Convert.ToInt32(dgwEmployees.CurrentRow.Cells[0].Value),
                FirstName = tbxEmployeeFNameAdd.Text,
                LastName = tbxLNameAddEmployee.Text,
                Age = Convert.ToInt32(tbxAgeAddEmployee.Text),
                Position = tbxPositionAdd.Text,
                Salary = Convert.ToDecimal(tbxSalaryAdd.Text)
            });
            MessageBox.Show("Updated!", "Message");
            LoadEmployees();
        }

        private void tbxIdSearchEmployee_TextChanged_1(object sender, EventArgs e)
        {

            var id = tbxIdSearchEmployee.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, id, () => { List<Employee> results = new List<Employee>(); results.Add(_employeeService.GetById(Convert.ToInt32(id))); dgwEmployees.DataSource = results; }, LoadEmployees);

        }

        private void tbxSearchFNameEmployee_TextChanged(object sender, EventArgs e)
        {
            var text = tbxSearchFNameEmployee.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, text, () => { dgwEmployees.DataSource = _employeeService.GetByFirstName(text); }, LoadEmployees);

        }

        private void tbxSearchLNameEmployee_TextChanged_1(object sender, EventArgs e)
        {
            var text = tbxSearchLNameEmployee.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, text, () => { dgwEmployees.DataSource = _employeeService.GetByLastName(text); }, LoadEmployees);

        }

        private void tbxSearchPositionEmployee_TextChanged_1(object sender, EventArgs e)
        {
            var text = tbxSearchPositionEmployee.Text;
            _utilities.Check(_utilities.IsNullOrEmpty, text, () => { dgwEmployees.DataSource = _employeeService.GetByPosition(text); }, LoadEmployees);

        }

        private void tbxMinAgeEmployee_TextChanged_1(object sender, EventArgs e)
        {
            _utilities.Check(_utilities.IsNullOrEmpty, tbxMinAgeEmployee.Text, LoadByAge, LoadEmployees);
        }

        private void tbxMaxAgeEmployee_TextChanged(object sender, EventArgs e)
        {
            _utilities.Check(_utilities.IsNullOrEmpty, tbxMaxAgeEmployee.Text, LoadByAge, LoadEmployees);

        }

        private void tbxMinSalaryEmployee_TextChanged_1(object sender, EventArgs e)
        {
            _utilities.Check(_utilities.IsNullOrEmpty, tbxMinSalaryEmployee.Text, LoadBySalary, LoadEmployees);
        }

        private void tbxMaxSalaryEmployee_TextChanged_1(object sender, EventArgs e)
        {
            _utilities.Check(_utilities.IsNullOrEmpty, tbxMaxSalaryEmployee.Text, LoadBySalary, LoadEmployees);
        }

        private void dgwEmployees_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            var text = dgwEmployees.CurrentRow.Cells;
            tbxFnameUpdateEmployee.Text = text[1].Value.ToString();
            tbxLNameUpdateEmployee.Text = text[2].Value.ToString();
            tbxAgeUpdateEmployee.Text = text[3].Value.ToString();
            tbxPositionUpdateEmployee.Text = text[4].Value.ToString();
            tbxSalaryUpdateEmployee.Text = text[5].Value.ToString();
        }
    }

}
