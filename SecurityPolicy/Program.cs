
using SecurityPolicy;
using SecurityPolicy.UI;

List<User> users = new()
{
    new User() {Id = 100, Name = "Admin"},
    new User() {Id = 0, Name = "Pavel"},
    new User() {Id = 1, Name = "Semen"},
    new User() {Id = 2, Name = "Igor"},
    new User() {Id = 3, Name = "Lia"},
    new User() {Id = 4, Name = "Simon"},
};

var admin = users.First(u => u.Id == 100);

List<AccessFile> files = new()
{
    new AccessFile() {Name = "Document"},
    new AccessFile() {Name = "Image"},
    new AccessFile() {Name = "File"},
    new AccessFile() {Name = "Program"},
};

var factory = new AccessMatrixFactory();
var app = new UI(admin, users, files, factory);

app.Start();
