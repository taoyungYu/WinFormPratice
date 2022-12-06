using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WinFormPratice;

public class Form1 : Form
{
    private const int User_ID = 0;
    private const string API_URL = "https://localhost:7287";

    public Form1()
    {
        InitialComponents();

        dataGridView1.CellClick += DataGridView1_CellClick;
        this.Shown += Form1_Shown;
    }

    private void InitialComponents()
    {
        this.AutoSize = true;
        this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        dataGridView1.AutoGenerateColumns = true;
        dataGridView1.ReadOnly = true;
        dataGridView1.AutoSize = true;
        dataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;

        DataGridViewButtonColumn buttonColumn =
            new DataGridViewButtonColumn();
        buttonColumn.Text = "Order";
        buttonColumn.UseColumnTextForButtonValue = true;
        buttonColumn.Name = "Order";

        this.Controls.Add(dataGridView1);
        dataGridView1.Columns.Add(buttonColumn);
    }

    private async void Form1_Shown(Object? sender, EventArgs e)
    {
        var events = await GetEvent();
        dataGridView1.DataSource = events;
        dataGridView1.Columns["Order"].DisplayIndex = dataGridView1.Columns.Count - 1;
    }

    private async void DataGridView1_CellClick(Object? sender, DataGridViewCellEventArgs e)
    {

        if (dataGridView1.Columns[e.ColumnIndex].Name == "Order")
        {
            var eventId = (int)dataGridView1.Rows[e.RowIndex].Cells["EventId"].Value;
            var message = await Order(User_ID, eventId);
            MessageBox.Show(message);
        }
    }

    private async Task<IList<Event>?> GetEvent()
    {
        var client = new HttpClient();
        var responseMessage = await client.GetAsync(API_URL + "/api/events");
        var contentString = await responseMessage.Content.ReadAsStringAsync();
        var events = JsonConvert.DeserializeObject<IList<Event>?>(contentString);
        return events;
    }

    private async Task<string> Order(int userId, int eventId)
    {
        var client = new HttpClient();
        var payload = new
        {
            UserId = userId,
            EventId = eventId
        };
        var payloadString = JsonConvert.SerializeObject(payload);
        var stringContent = new StringContent(payloadString);
        stringContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        var responseMessage = await client.PostAsync(API_URL + "/api/orders", stringContent);
        if (responseMessage.StatusCode == HttpStatusCode.OK)
        {
            return "Success!";
        }
        var contentString = await responseMessage.Content.ReadAsStringAsync();
        return contentString;
    }

    private DataGridView dataGridView1 = new DataGridView();
    private BindingSource bindingSource1 = new BindingSource();
}
