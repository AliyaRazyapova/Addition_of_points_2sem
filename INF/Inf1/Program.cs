var studentsBySchool = File.ReadAllLines("data.txt")
    .Select(
        x =>
        {
            var temp = x.Split(' ');
            return new Student(
                temp[3],
                temp[4],
                int.Parse(temp[5]),
                int.Parse(temp[2]));
        })
    .GroupBy(x => x.SchoolNumber, x => x, (i, students) => students.ToArray());

var answer = studentsBySchool.Select(
    x =>
    {
        var max = x.Max(y => y.InfEge);
        return x.First(y => y.InfEge == max);
    })
    .OrderBy(x => x.SchoolNumber);

Console.WriteLine(string.Join('\n', answer));

record Student(string Surname, string Initials, int SchoolNumber, int InfEge)
{
    public override string ToString()
     => $"{SchoolNumber} {Surname} {Initials} {InfEge}";
};
