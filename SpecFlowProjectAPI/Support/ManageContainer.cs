using ProyectoMock;
using Testcontainers.PostgreSql;

public class ManageContainer
{
    private PostgreSqlContainer _postgres;
    private UserService _userService;

    public async Task StartContainerAsync()
    {
        _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .Build();

        await _postgres.StartAsync();
    }

    public UserService GetUserService()
    {
        var dbConnectionProvider = new DbConnectionProvider(_postgres.GetConnectionString());
        _userService = new UserService(dbConnectionProvider.GetConnection());
        return _userService;
    }

    public async Task CreateTableForUserAsync()
    {
        var createTableSql = @"
            CREATE TABLE IF NOT EXISTS Customers (
                Id INT PRIMARY KEY,
                Name TEXT NOT NULL,
                Email TEXT NOT NULL
            );";
        _userService.ExecuteNonQuery(createTableSql);
    }

    [AfterScenario]
    public async Task StopContainerAsync()
    {
        await _postgres.StopAsync();
    }
}
