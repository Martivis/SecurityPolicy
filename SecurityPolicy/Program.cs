
using SecurityPolicy;

User admin = new()
{
    Id = 100,
    Name = "Lesly"
};

List<User> users = new()
{
    new User() {Id = 0, Name = "Pavel"},
    new User() {Id = 1, Name = "Semen"},
    new User() {Id = 2, Name = "Igor"},
    new User() {Id = 3, Name = "Lia"},
    new User() {Id = 4, Name = "Simon"},
};

List<AccessFile> files = new()
{
    new AccessFile() {Name = "Document"},
    new AccessFile() {Name = "Image"},
    new AccessFile() {Name = "File"},
    new AccessFile() {Name = "Program"},
};

var matrixFactory = new AccessMatrixFactory();
var accessMatrix = matrixFactory.Create(admin, users, files);


Console.WriteLine(accessMatrix.CheckRight(users[0], files[0], Right.Read));
