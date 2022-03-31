using Microsoft.EntityFrameworkCore;

using var db = new Context();
var script = db.Database.GenerateCreateScript();

Console.WriteLine(script);
