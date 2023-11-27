
using SecurityPolicy;
using SecurityPolicy.UI;

List<User> users = new()
{
    new User() {Id = 100, Name = "Admin"},
    new User() {Id = 0, Name = "Pavel"},
    new User() {Id = 1, Name = "Semen"},
};

var admin = users.First(u => u.Id == 100);

List<AccessFile> files = new()
{
    new AccessFile() {Name = "Document"},
    new AccessFile() {Name = "Image"},
    new AccessFile() {Name = "File"},
    new AccessFile() {Name = "Program"},
    new AccessFile() {Name = "Game"}
};

var factory = new AccessMatrixFactory();
var app = new UI(admin, users, files, factory);

app.Start();
