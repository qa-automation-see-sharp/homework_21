using Microsoft.Playwright;

namespace Test.Utils.PageObjects;

public class WebTablePage : IBasePage
{
    public IPage? Page { get; set; }
    public string Url { get; } = "https://demoqa.com/webtables";
    public string ExpectedTitle { get; } = "Web Tables";

    public ILocator Title => Page!.Locator("xpath=//h1[text()='Web Tables']");
    public ILocator Table => Page!.Locator("//div[@class='rt-tbody']");
    public string TableXPath => "//div[@class='rt-tbody']";
    public ILocator DeleteButton => Page!.Locator("delete-record-1");
    public ILocator AgeColumnHeader => Page!.Locator(".ReactTable .rt-thead .rt-th:nth-child(3)");
    public ILocator AddButton => Page!.Locator("addNewRecordButton");
    public ILocator SubmitButton => Page!.Locator("submit");
    public ILocator RegistrationForm => Page!.Locator("registration-form-modal");
    public ILocator FirstNameInput => Page!.Locator("firstName");
    public ILocator LastNameInput => Page!.Locator("lastName");
    public ILocator EmailInput => Page!.Locator("userEmail");
    public ILocator AgeInput => Page!.Locator("age");
    public ILocator SalaryInput => Page!.Locator("salary");
    public ILocator DepartmentInput => Page!.Locator("department");

    public string firstName = "Firstname";
    public string lastName = "Lastname";
    public string email = "email@email.com";
    public string age = "0";
    public string salary = "0";
    public string department = "Department";

    public async Task<WebTablePage> Open()
    {
        await Page!.GotoAsync(Url);
        return this;
    }

    public async Task<WebTablePage> FillOutRegistrationForm()
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await EmailInput.FillAsync(email);
        await AgeInput.FillAsync(age);
        await SalaryInput.FillAsync(salary);
        await DepartmentInput.FillAsync(department);
        return this;
    }

    public async Task<string> GetCellTextAsync(IPage page, string tableSelector, int rowIndex, int columnIndex)
    {
        var rows = await page.QuerySelectorAllAsync($"xpath={tableSelector}/tr");

        if (rowIndex < rows.Count)
        {
            var cells = await rows[rowIndex].QuerySelectorAllAsync("td, th");

            if (columnIndex < cells.Count)
            {
                return await cells[columnIndex].InnerTextAsync();
            }
        }

        return null;
    }

    public async Task<int> GetRowCountAsync(IPage page, string tableSelector)
    {
        var rows = await page.QuerySelectorAllAsync($"xpath={tableSelector}/tr");
        return rows.Count;
    }

    public async Task<int> GetColumnCountAsync(IPage page, string tableSelector)
    {
        var firstRow = await page.QuerySelectorAsync($"xpath={tableSelector}/tr");

        if (firstRow != null)
        {
            var columns = await firstRow.QuerySelectorAllAsync("td, th");
            return columns.Count;
        }

        return 0;
    }

    public async Task<List<int>> GetColumnDataAsIntegersAsync(IPage page, string tableSelector, int columnIndex)
    {
        var columnData = new List<int>();
        var rows = await page.QuerySelectorAllAsync($"xpath={tableSelector}/tr");

        foreach (var row in rows)
        {
            var cells = await row.QuerySelectorAllAsync("td, th");

            if (cells.Count > columnIndex)
            {
                var cellText = await cells[columnIndex].InnerTextAsync();

                if (int.TryParse(cellText, out int cellValue))
                {
                    columnData.Add(cellValue);
                }
            }
        }

        return columnData;
    }

    public bool IsSortedAscending(List<int> list)
    {
        return list.SequenceEqual(list.OrderBy(x => x));
    }

    public bool IsSortedDescending(List<int> list)
    {
        return list.SequenceEqual(list.OrderByDescending(x => x));
    }
}
