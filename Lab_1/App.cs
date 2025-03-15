using MyFont = iTextSharp.text.Font;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Org.BouncyCastle.Math;


namespace Lab_1
{
    public partial class App : Form
    {
        string connectionString = @"Data Source=ALEX-DESKTOP;Initial Catalog=TravelAgency;Integrated Security=True";
        string currentZvit = "";
        string currentZvit1 = "";
        public App()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void clientInsertButton_Click(object sender, EventArgs e)
        {
            string name = nameInput.Text;
            string surname = surnameInput.Text;
            string patronymic = patronymicInput.Text;
            string address = addressInput.Text;
            string phone = phoneInput.Text;

            Client client = new Client(name, surname, patronymic, address, phone);
            try
            {
                Validator.ValidateClient(client);

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Clients (surname,name,patronymic,address,phone) VALUES (@Surname, @Name, @Patro, @Address, @Phone)";

                        cmd.Parameters.AddWithValue("@Surname", client.Surname);
                        cmd.Parameters.AddWithValue("@Name", client.Name);
                        cmd.Parameters.AddWithValue("@Patro", client.Patronymic);
                        cmd.Parameters.AddWithValue("@Address", client.Address);
                        cmd.Parameters.AddWithValue("@Phone", client.Phone);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            clientStatusStripLabel.Text = "Added client";
                        }
                        else
                        {
                            clientStatusStripLabel.Text = "Nothing added";
                        }
                    }

                    sql.Close();
                }

            }
            catch (Exception ex)
            {
                clientStatusStripLabel.Text = ex.Message;
            }
        }
        private void routeInsertButton_Click(object sender, EventArgs e)
        {
            string country = countryInput.Text;
            string climate = climateInput.Text;
            if (!int.TryParse(durationInput.Text, out int duration)) duration = -1;
            string hotel = hotelInput.Text;
            if (!int.TryParse(priceInput.Text, out int price)) price = -1;

            Route route = new Route(country, climate, duration, hotel, price);
            try
            {
                Validator.ValidateRoute(route);

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Routes (country,climate,duration,hotel,price) VALUES (@Country, @Climate, @Dur, @Hotel, @Price)";

                        cmd.Parameters.AddWithValue("@Country", route.Country);
                        cmd.Parameters.AddWithValue("@Climate", route.Climate);
                        cmd.Parameters.AddWithValue("@Dur", route.Duration);
                        cmd.Parameters.AddWithValue("@Hotel", route.Hotel);
                        cmd.Parameters.AddWithValue("@Price", route.Money);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            routeStatusStripLabel.Text = "Added route";
                        }
                        else
                        {
                            routeStatusStripLabel.Text = "Nothing added";
                        }
                    }

                    sql.Close();
                }

            }
            catch (Exception ex)
            {
                routeStatusStripLabel.Text = ex.Message;
            }
        }
        private void travelInsertButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(routeIdInput.Text, out int routeId)) routeId = -1;
            if (!int.TryParse(clientIdInput.Text, out int clientId)) clientId = -1;
            DateTime date = dateInput.Value;
            if (!int.TryParse(amountInput.Text, out int amount)) amount = -1;
            if (!int.TryParse(discountInput.Text, out int discount)) discount = -1;
            if (!int.TryParse(costInput.Text, out int cost)) cost = -1;

            Travel travel = new Travel(routeId, clientId, date, amount, discount, cost);
            try
            {
                Validator.ValidateTravel(travel);

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Travels (route_id,client_id,travel_date,amount,discount,cost) VALUES (@RouteId, @ClientId, @Date, @Amount, @Dis, @Cost)";
                        cmd.Parameters.AddWithValue("@RouteId", travel.RouteId);
                        cmd.Parameters.AddWithValue("@ClientId", travel.ClientId);
                        cmd.Parameters.AddWithValue("@Date", travel.Date);
                        cmd.Parameters.AddWithValue("@Amount", travel.Amount);
                        cmd.Parameters.AddWithValue("@Dis", travel.Discount);
                        cmd.Parameters.AddWithValue("@Cost", travel.Cost);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            travelStatusStripLabel.Text = "Added travel";
                        }
                        else
                        {
                            travelStatusStripLabel.Text = "Nothing added";
                        }
                    }

                    sql.Close();
                }

            }
            catch (Exception ex)
            {
                travelStatusStripLabel.Text = ex.Message;
            }
        }

        private void clientDeleteButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(deleteClientIdInput.Text, out int id)) id = -1;

            try
            {
                if (id < 0)
                {
                    throw new Exception("Invalid client id");
                }

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Clients WHERE client_id=@Id";
                        cmd.Parameters.AddWithValue("@Id", id);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            deleteClientStatusStripLabel.Text = "Client object deleted";
                        }
                        else
                        {
                            deleteClientStatusStripLabel.Text = "Nothing deleted";
                        }
                    }

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                deleteClientStatusStripLabel.Text = ex.Message;
            }
        }
        private void routeDeleteButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(deleteRouteIdInput.Text, out int id)) id = -1;

            try
            {
                if (id < 0)
                {
                    throw new Exception("Invalid route id");
                }

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Routes WHERE route_id=@Id";
                        cmd.Parameters.AddWithValue("@Id", id);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            deleteRouteStatusStripLabel.Text = "Route object deleted";
                        }
                        else
                        {
                            deleteRouteStatusStripLabel.Text = "Nothing deleted";
                        }
                    }

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                deleteRouteStatusStripLabel.Text = ex.Message;
            }
        }
        private void travelDeleteButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(deleteTravelIdInput.Text, out int id)) id = -1;

            try
            {
                if (id < 0)
                {
                    throw new Exception("Invalid travel id");
                }

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Travels WHERE travel_id=@Id";
                        cmd.Parameters.AddWithValue("@Id", id);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            deleteTravelStatusStripLabel.Text = "Travel object deleted";
                        }
                        else
                        {
                            deleteTravelStatusStripLabel.Text = "Nothing deleted";
                        }
                    }

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                deleteTravelStatusStripLabel.Text = ex.Message;
            }
        }

        private void clientUpdateButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(idClientUpdateInput.Text, out int id)) id = -1;
            string name = nameUpdateInput.Text;
            string surname = surnameUpdateInput.Text;
            string patronymic = patronymicUpdateInput.Text;
            string address = addressUpdateInput.Text;
            string phone = phoneUpdateInput.Text;

            Client client = new Client(id, name, surname, patronymic, address, phone);
            try
            {
                Validator.ValidateClient(client);

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Clients SET surname=@Surname, name=@Name, patronymic=@Patro, address=@Address, phone=@Phone WHERE client_id=@Id";

                        cmd.Parameters.AddWithValue("@Id", client.Id);
                        cmd.Parameters.AddWithValue("@Surname", client.Surname);
                        cmd.Parameters.AddWithValue("@Name", client.Name);
                        cmd.Parameters.AddWithValue("@Patro", client.Patronymic);
                        cmd.Parameters.AddWithValue("@Address", client.Address);
                        cmd.Parameters.AddWithValue("@Phone", client.Phone);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            updateClientStripStatusLabel.Text = "Updated client";
                        }
                        else
                        {
                            updateClientStripStatusLabel.Text = "Nothing updated";
                        }
                    }

                    sql.Close();
                }

            }
            catch (Exception ex)
            {
                updateClientStripStatusLabel.Text = ex.Message;
            }
        }
        private void routeUpdateButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(idRouteUpdateInput.Text, out int id)) id = -1;

            string country = countryUpdateInput.Text;
            string climate = climateUpdateInput.Text;
            if (!int.TryParse(durationUpdateInput.Text, out int duration)) duration = -1;
            string hotel = hotelUpdateInput.Text;

            if (!double.TryParse(priceUpdateInput.Text, out double tmpPrice)) tmpPrice = -1;
            int price = (int)tmpPrice;

            Route route = new Route(id, country, climate, duration, hotel, price);
            try
            {
                Validator.ValidateRoute(route);

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Routes SET country=@Country, climate=@Climate, duration=@Dur, hotel=@Hotel, price=@Price WHERE route_id=@Id";

                        cmd.Parameters.AddWithValue("@Id", route.Id);
                        cmd.Parameters.AddWithValue("@Country", route.Country);
                        cmd.Parameters.AddWithValue("@Climate", route.Climate);
                        cmd.Parameters.AddWithValue("@Dur", route.Duration);
                        cmd.Parameters.AddWithValue("@Hotel", route.Hotel);
                        cmd.Parameters.AddWithValue("@Price", route.Money);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            updateRouteStripStatusLabel.Text = "Updated route";
                        }
                        else
                        {
                            updateRouteStripStatusLabel.Text = "Nothing updated";
                        }
                    }

                    sql.Close();
                }

            }
            catch (Exception ex)
            {
                updateRouteStripStatusLabel.Text = ex.Message;
            }
        }
        private void travelUpdateButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(idTravelUpdateInput.Text, out int id)) id = -1;

            if (!int.TryParse(routeIdUpdateInput.Text, out int routeId)) routeId = -1;
            if (!int.TryParse(clientIdUpdateInput.Text, out int clientId)) clientId = -1;
            DateTime date = dateUpdateInput.Value;
            if (!int.TryParse(amountUpdateInput.Text, out int amount)) amount = -1;
            if (!int.TryParse(discountUpdateInput.Text, out int discount)) discount = -1;

            if (!double.TryParse(costUpdateInput.Text, out double tmpCost)) tmpCost = -1;
            int cost = (int)tmpCost;

            Travel travel = new Travel(id, routeId, clientId, date, amount, discount, cost);
            try
            {
                Validator.ValidateTravel(travel);

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Travels SET route_id=@RouteId, client_id=@ClientId, travel_date=@Date, amount=@Amount, discount=@Dis, cost=@Cost WHERE travel_id=@Id";

                        cmd.Parameters.AddWithValue("@Id", travel.Id);
                        cmd.Parameters.AddWithValue("@RouteId", travel.RouteId);
                        cmd.Parameters.AddWithValue("@ClientId", travel.ClientId);
                        cmd.Parameters.AddWithValue("@Date", travel.Date);
                        cmd.Parameters.AddWithValue("@Amount", travel.Amount);
                        cmd.Parameters.AddWithValue("@Dis", travel.Discount);
                        cmd.Parameters.AddWithValue("@Cost", travel.Cost);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            updateTravelStripStatusLabel.Text = "Updated travel";
                        }
                        else
                        {
                            updateTravelStripStatusLabel.Text = "Nothing updated";
                        }
                    }

                    sql.Close();
                }

            }
            catch (Exception ex)
            {
                updateTravelStripStatusLabel.Text = ex.Message;
            }
        }
        private void clientSearchButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(idClientUpdateInput.Text, out int id)) id = -1;

            try
            {
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM Clients WHERE client_id={id}", sql);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        updateClientStripStatusLabel.Text = "Returned results";
                        DataRow row = dataTable.Rows[0];
                        nameUpdateInput.Text = row["name"].ToString();
                        surnameUpdateInput.Text = row["surname"].ToString();
                        patronymicUpdateInput.Text = row["patronymic"].ToString();
                        addressUpdateInput.Text = row["address"].ToString();
                        phoneUpdateInput.Text = row["phone"].ToString();
                    }
                    else
                    {
                        clientUpdateRefresh_Click(sender, e);
                        updateClientStripStatusLabel.Text = "Nothing returned";
                    }

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                updateClientStripStatusLabel.Text = ex.Message;
            }
        }
        private void routeSearchButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(idRouteUpdateInput.Text, out int id)) id = -1;

            try
            {
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM Routes WHERE route_id={id}", sql);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        updateRouteStripStatusLabel.Text = "Returned results";
                        DataRow row = dataTable.Rows[0];
                        countryUpdateInput.Text = row["country"].ToString();
                        climateUpdateInput.Text = row["climate"].ToString();
                        durationUpdateInput.Text = row["duration"].ToString();
                        hotelUpdateInput.Text = row["hotel"].ToString();
                        priceUpdateInput.Text = row["price"].ToString();
                    }
                    else
                    {
                        routeUpdateRefresh_Click(sender, e);
                        updateRouteStripStatusLabel.Text = "Nothing returned";
                    }

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                updateRouteStripStatusLabel.Text = ex.Message;
            }
        }
        private void travelSearchButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(idTravelUpdateInput.Text, out int id)) id = -1;

            try
            {
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM Travels WHERE travel_id={id}", sql);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        updateTravelStripStatusLabel.Text = "Returned results";
                        DataRow row = dataTable.Rows[0];
                        routeIdUpdateInput.Text = row["route_id"].ToString();
                        clientIdUpdateInput.Text = row["client_id"].ToString();
                        dateUpdateInput.Text = row["travel_date"].ToString();
                        amountUpdateInput.Text = row["amount"].ToString();
                        discountUpdateInput.Text = row["discount"].ToString();
                        costUpdateInput.Text = row["cost"].ToString();
                    }
                    else
                    {
                        travelUpdateRefresh_Click(sender, e);
                        updateTravelStripStatusLabel.Text = "Nothing returned";
                    }

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                updateTravelStripStatusLabel.Text = ex.Message;
            }
        }

        private void selectFromDB(string query)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(query, sql);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        selectStatusStripLabel.Text = "Returned results";
                    }
                    else
                    {
                        selectStatusStripLabel.Text = "Nothing returned";
                    }

                    dataGridView.DataSource = dataTable;

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                selectStatusStripLabel.Text = ex.Message;
            }
        }
        private void selectClientsButton_Click(object sender, EventArgs e)
        {
            currentZvit = "�볺���";
            selectFromDB("SELECT client_id as 'Id',surname as '�������',name as '���',patronymic as '��-�������',address as '������',phone as '�������' FROM Clients");
        }
        private void selectRoutesButton_Click(object sender, EventArgs e)
        {
            currentZvit = "��������";
            selectFromDB("SELECT route_id as 'Id',country as '�����',climate as '�����',duration as '���������',hotel as '������',price as 'ֳ��' FROM Routes");
        }
        private void selectTravelsButton_Click(object sender, EventArgs e)
        {
            currentZvit = "������";
            selectFromDB("SELECT travel_id as 'Id',route_id as '�������',client_id as '�볺��',travel_date as '����',amount as 'ʳ������',discount as '������',cost as '�������' FROM Travels");
        }

        private void refreshClientButton_Click(object sender, EventArgs e)
        {
            nameInput.Text = "";
            surnameInput.Text = "";
            patronymicInput.Text = "";
            addressInput.Text = "";
            phoneInput.Text = "";
            clientStatusStripLabel.Text = "Form refreshed";
        }
        private void refreshRouteButton_Click(object sender, EventArgs e)
        {
            countryInput.Text = "";
            climateInput.Text = "";
            durationInput.Text = "";
            hotelInput.Text = "";
            priceInput.Text = "";
            routeStatusStripLabel.Text = "Form refreshed";
        }
        private void refreshTravelButton_Click(object sender, EventArgs e)
        {
            routeIdInput.Text = "";
            clientIdInput.Text = "";
            dateInput.Value = DateTime.Now;
            amountInput.Text = "";
            discountInput.Text = "";
            costInput.Text = "";
            travelStatusStripLabel.Text = "Form refreshed";
        }
        private void clientUpdateRefresh_Click(object sender, EventArgs e)
        {
            idClientUpdateInput.Text = "";
            nameUpdateInput.Text = "";
            surnameUpdateInput.Text = "";
            patronymicUpdateInput.Text = "";
            addressUpdateInput.Text = "";
            phoneUpdateInput.Text = "";
            updateClientStripStatusLabel.Text = "Form refreshed";
        }
        private void routeUpdateRefresh_Click(object sender, EventArgs e)
        {
            idRouteUpdateInput.Text = "";
            countryUpdateInput.Text = "";
            climateUpdateInput.Text = "";
            durationUpdateInput.Text = "";
            hotelUpdateInput.Text = "";
            priceUpdateInput.Text = "";
            updateRouteStripStatusLabel.Text = "Form refreshed";
        }
        private void travelUpdateRefresh_Click(object sender, EventArgs e)
        {
            idTravelUpdateInput.Text = "";
            routeIdUpdateInput.Text = "";
            clientIdUpdateInput.Text = "";
            dateUpdateInput.Value = DateTime.Now;
            amountUpdateInput.Text = "";
            discountUpdateInput.Text = "";
            costUpdateInput.Text = "";
            updateTravelStripStatusLabel.Text = "Form refreshed";
        }

        private void selectStreetButton_Click(object sender, EventArgs e)
        {
            currentZvit = "�볺��� �� ������ ��������";
            selectFromDB("SELECT client_id as 'Id',surname as '�������',name as '���',patronymic as '��-�������',address as '������',phone as '�������' FROM Clients WHERE address LIKE '���. ��������%'");
        }
        private void selectClimateButton_Click(object sender, EventArgs e)
        {
            currentZvit = "�������� � ������� �������";
            selectFromDB("SELECT route_id as 'Id',country as '�����',climate as '�����',duration as '���������',hotel as '������',price as 'ֳ��' FROM Routes WHERE climate = '�������'");
        }
        private void selectPriceButton_Click(object sender, EventArgs e)
        {
            currentZvit = "�������� �� �����";
            selectFromDB("SELECT route_id as 'Id',country as '�����',climate as '�����',duration as '���������',hotel as '������',price as 'ֳ��' FROM Routes WHERE price < 30000");
        }
        private void selectDurationButton_Click(object sender, EventArgs e)
        {
            currentZvit = "�������� �� ���������";
            selectFromDB("SELECT route_id as 'Id',country as '�����',climate as '�����',duration as '���������',hotel as '������',price as 'ֳ��' FROM Routes WHERE duration <= 7");
        }
        private void selectMonthButton_Click(object sender, EventArgs e)
        {
            currentZvit = "������� ������";
            selectFromDB("SELECT travel_id as 'Id',route_id as '�������',client_id as '�볺��',travel_date as '����',amount as 'ʳ������',discount as '������',cost as '�������' FROM Travels WHERE travel_date LIKE '%-10-%'");
        }
        private void selectCostButton_Click(object sender, EventArgs e)
        {
            currentZvit = "������ �� �������";
            selectFromDB("SELECT travel_id as 'Id',route_id as '�������',client_id as '�볺��',travel_date as '����',amount as 'ʳ������',discount as '������',cost as '�������' FROM Travels WHERE cost > 30000 and cost < 90000");
        }
        private void selectManyPeopleButton_Click(object sender, EventArgs e)
        {
            currentZvit = "����� ������";
            selectFromDB("SELECT travel_id as 'Id',route_id as '�������',client_id as '�볺��',travel_date as '����',amount as 'ʳ������',discount as '������',cost as '�������' FROM Travels WHERE amount >= 5");
        }
        private void selectBigDiscountButton_Click(object sender, EventArgs e)
        {
            currentZvit = "������ ������";
            selectFromDB("SELECT travel_id as 'Id',route_id as '�������',client_id as '�볺��',travel_date as '����',amount as 'ʳ������',discount as '������',cost as '�������' FROM Travels WHERE discount >= 15");
        }
        private void selectPriceDurationButton_Click(object sender, EventArgs e)
        {
            currentZvit = "����� ������";
            selectFromDB("SELECT TOP(3) route_id as 'Id',country as '�����',climate as '�����',duration as '���������',hotel as '������',price as 'ֳ��' FROM Routes WHERE duration > 7 and price < 40000 ORDER BY price");
        }
        private void selectSurnameSortButton_Click(object sender, EventArgs e)
        {
            currentZvit = "�볺��� �� ��������";
            selectFromDB("SELECT client_id as 'Id',surname as '�������',name as '���',patronymic as '��-�������',address as '������',phone as '�������' FROM Clients ORDER BY surname");
        }

        private void clientRouteJoinButton_Click(object sender, EventArgs e)
        {
            currentZvit = "������(����)";
            selectFromDB("SELECT Clients.surname as '�������', Clients.name as '���', Routes.country as '�����', Routes.hotel as '������', Travels.amount as '�-���', Travels.cost as '�������' FROM Travels JOIN Clients ON Travels.client_id = Clients.client_id JOIN Routes ON Travels.route_id = Routes.route_id");
        }
        private void travelCountButton_Click(object sender, EventArgs e)
        {
            currentZvit = "�-��� ������";
            selectFromDB("SELECT Clients.surname as '�������', Clients.name as '���', COUNT(Travels.travel_id) as '�-��� ������' FROM Travels JOIN Clients ON Travels.client_id = Clients.client_id GROUP BY Clients.surname, Clients.name ORDER BY COUNT(Travels.travel_id) DESC");
        }
        private void travelTimeButton_Click(object sender, EventArgs e)
        {
            currentZvit = "���������";
            selectFromDB("SELECT Clients.surname as '�������', Clients.name as '���', Travels.travel_date as '����', Routes.duration as '���������' FROM Travels JOIN Clients ON Travels.client_id = Clients.client_id JOIN Routes ON Travels.route_id = Routes.route_id");
        }
        private void climateCountButton_Click(object sender, EventArgs e)
        {
            currentZvit = "������";
            selectFromDB("SELECT climate as '�����', Count(climate) as '�-��� ��������' FROM Routes GROUP BY climate");
        }
        private void countryMinMaxCostButton_Click(object sender, EventArgs e)
        {
            currentZvit = "̳����� ֳ��";
            selectFromDB("SELECT Routes.country as '�����', MIN(Travels.cost) as '̳� �������', MAX(Travels.cost) as '���� �������' FROM Travels JOIN Routes ON Travels.route_id = Routes.route_id GROUP BY Routes.country");
        }
        private void countryCountButton_Click(object sender, EventArgs e)
        {
            currentZvit = "�����";
            selectFromDB("SELECT Routes.country as '�����', COUNT(Travels.travel_id) as '�-��� ������', SUM(Travels.cost) as '��� �������' FROM Travels JOIN Routes ON Travels.route_id = Routes.route_id GROUP BY Routes.country");
        }
        private void discountAmountButton_Click(object sender, EventArgs e)
        {
            currentZvit = "������ �� �-��";
            selectFromDB("SELECT discount as '������', AVG(CAST(amount as float)) as '�� �-��� ����� ��� ������' FROM Travels GROUP BY discount");
            dataGridView.Columns["�� �-��� ����� ��� ������"].DefaultCellStyle.Format = "F2";
        }

        private void selectFromDB2(string query)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(query, sql);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        selectParamStatusStripLabel.Text = "Returned results";
                    }
                    else
                    {
                        selectParamStatusStripLabel.Text = "Nothing returned";
                    }

                    dataGridView1.DataSource = dataTable;

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                selectParamStatusStripLabel.Text = ex.Message;
            }
        }
        private void inputCountryButton_Click(object sender, EventArgs e)
        {
            if (inputCountryInput.Text.Length == 0)
            {
                selectParamStatusStripLabel.Text = "Invalid input";
                return;
            }
            currentZvit1 = $"�������� � {inputCountryInput.Text}";
            selectFromDB2($"SELECT route_id as 'Id',country as '�����',climate as '�����',duration as '���������',hotel as '������',price as 'ֳ��' FROM Routes WHERE country LIKE '{inputCountryInput.Text}%'");
        }
        private void inputPriceButton_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(inputPriceInput.Text, out double price))
            {
                selectParamStatusStripLabel.Text = "Invalid input";
                return;
            }
            currentZvit1 = $"�������� �� {price}";
            selectFromDB2($"SELECT route_id as 'Id',country as '�����',climate as '�����',duration as '���������',hotel as '������',price as 'ֳ��' FROM Routes WHERE price < {price}");
        }
        private void inputDateButton_Click(object sender, EventArgs e)
        {
            currentZvit1 = $"������ �� [{lowerDateInput.Value};{upperDateInput.Value}]";
            selectFromDB2($"SELECT Clients.surname as '�������', Clients.name as '���', Routes.country as '�����', Routes.hotel as '������', Travels.travel_date as '���� ��������' FROM Travels JOIN Clients ON Travels.client_id = Clients.client_id JOIN Routes ON Travels.route_id = Routes.route_id WHERE Travels.travel_date BETWEEN '{lowerDateInput.Value}' AND '{upperDateInput.Value}' ORDER BY Travels.travel_date");
        }
        private void popRoutesButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(popRoutesInput.Text, out int count))
            {
                selectParamStatusStripLabel.Text = "Invalid input";
                return;
            }
            currentZvit1 = $"�������� > {count}";
            selectFromDB2($"SELECT Routes.country as '�����', Routes.hotel as '������', COUNT(Travels.travel_id) as '�-���' FROM Travels JOIN Routes ON Travels.route_id = Routes.route_id GROUP BY Routes.country, Routes.hotel HAVING COUNT(Travels.travel_id) >= {count} ORDER BY COUNT(Travels.travel_id) DESC");
        }
        private void inputSurnameButton_Click(object sender, EventArgs e)
        {
            if (inputSurnameInput.Text.Length == 0)
            {
                selectParamStatusStripLabel.Text = "Invalid input";
                return;
            }
            currentZvit1 = $"������ �� {inputSurnameInput.Text}";
            selectFromDB2($"SELECT Clients.surname as '�������', Clients.name as '���', Routes.country as '�����', Routes.hotel as '������', Travels.travel_date as '���� ��������' FROM Travels JOIN Clients ON Travels.client_id = Clients.client_id JOIN Routes ON Travels.route_id = Routes.route_id WHERE Clients.surname LIKE '{inputSurnameInput.Text}%'");
        }
        private void popClientsButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(popClientsInput.Text, out int count))
            {
                selectParamStatusStripLabel.Text = "Invalid input";
                return;
            }
            currentZvit1 = $"�볺��� > {count}";
            selectFromDB2($"SELECT Clients.surname as '�������', Clients.name as '���', COUNT(Travels.travel_id) as '�-���' FROM Travels JOIN Clients ON Travels.client_id = Clients.client_id GROUP BY Clients.surname, Clients.name HAVING COUNT(Travels.travel_id) >= {count} ORDER BY COUNT(Travels.travel_id) DESC");
        }
        private void bigTravelsButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(bigTravelsInput.Text, out int count))
            {
                selectParamStatusStripLabel.Text = "Invalid input";
                return;
            }
            currentZvit1 = $"������ � > {count}";
            selectFromDB2($"SELECT Clients.surname as '�������', Clients.name as '���', Routes.country as '�����', Routes.hotel as '������', Travels.amount as '�-��� �����' FROM Travels JOIN Clients ON Travels.client_id = Clients.client_id JOIN Routes ON Travels.route_id = Routes.route_id WHERE Travels.amount >= {count}");
        }

        public void ExportToPdf(DataTable dataTable, string filePath, ToolStripStatusLabel label)
        {
            try
            {
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new InvalidOperationException("DataTable is empty.");
                }

                BaseFont baseFont = BaseFont.CreateFont(@"C:\Users\User\Desktop\����\3�1�\��\��� 2\timesnewromanpsmt.ttf",
                    BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                MyFont font = new MyFont(baseFont, 12, MyFont.NORMAL);

                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    Paragraph title = new Paragraph("��� ���� ������", new MyFont(baseFont, 16, MyFont.BOLD));
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    pdfDoc.Add(new Paragraph("\n"));

                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
                    pdfTable.WidthPercentage = 100;

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, font))
                        {
                            BackgroundColor = BaseColor.LIGHT_GRAY
                        };
                        pdfTable.AddCell(cell);
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            pdfTable.AddCell(new Phrase(item.ToString(), font));
                        }
                    }

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                }
            }
            catch (Exception ex)
            {
                label.Text = ex.Message;
            }
        }
        private void zvitButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = $"{currentZvit}.pdf";
                saveFileDialog.InitialDirectory = @"C:\Users\User\Desktop\����\3�1�\��\��� 2";
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "�������� ���";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToPdf((DataTable)dataGridView.DataSource, Path.Combine(saveFileDialog.InitialDirectory, saveFileDialog.FileName), selectStatusStripLabel);
                }
                else
                {
                    selectStatusStripLabel.Text = "Error";
                }
            }

        }
        private void zvit1Button_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = $"{currentZvit1}.pdf";
                saveFileDialog.InitialDirectory = @"C:\Users\User\Desktop\����\3�1�\��\��� 2";
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "�������� ���";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToPdf((DataTable)dataGridView1.DataSource, Path.Combine(saveFileDialog.InitialDirectory, saveFileDialog.FileName), selectParamStatusStripLabel);
                }
                else
                {
                    selectStatusStripLabel.Text = "Error";
                }
            }
        }
    }
}