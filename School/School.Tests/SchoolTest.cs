using School.Classes;
namespace School.Tests;

public class SchoolTest
{
    /// <summary>
    /// �������� ������ �� 8 ������
    /// </summary>
    /// <returns></returns>
    private List<Grade> CreateListGrades()
    {
        var students = CreateListStudents();
        var subject = CreateListSubjects();
        return new List<Grade>()
        {
            new Grade(subject[0], students[0], 5, DateTime.Parse("2022/10/10")),
            new Grade(subject[0], students[1], 5, DateTime.Parse("2022/10/10")),
            new Grade(subject[0], students[2], 5, DateTime.Parse("2022/10/10")),
            new Grade(subject[0], students[3], 5, DateTime.Parse("2022/10/10")),
            new Grade(subject[0], students[4], 5, DateTime.Parse("2022/10/10")),

            new Grade(subject[0], students[5], 2, DateTime.Parse("2022/12/12")),
            new Grade(subject[0], students[6], 3, DateTime.Parse("2022/12/12")),
            new Grade(subject[0], students[7], 4, DateTime.Parse("2022/12/12")),

            new Grade(subject[1], students[0], 3, DateTime.Parse("2022/12/13")),
            new Grade(subject[1], students[1], 4, DateTime.Parse("2022/10/13")),
            new Grade(subject[1], students[2], 4, DateTime.Parse("2022/10/13")),
            new Grade(subject[1], students[3], 4, DateTime.Parse("2022/10/13")),
            new Grade(subject[1], students[4], 4, DateTime.Parse("2022/10/12")),
            new Grade(subject[0], students[5], 1, DateTime.Parse("2022/10/13")),
            new Grade(subject[0], students[6], 1, DateTime.Parse("2022/10/13")),
            new Grade(subject[0], students[7], 1, DateTime.Parse("2022/10/13"))
        };
    }

    /// <summary>
    /// �������� ������ �� 8 ���������
    /// </summary>
    /// <returns></returns>
    private List<Subject> CreateListSubjects()
    {
        return new List<Subject>()
        {
            new Subject("���������", 2008,null),
            new Subject("������", 2020,null),
            new Subject("����������������", 2023,null),
            new Subject("�������������� ������", 2008,null),
            new Subject("�������", 2001,null),
            new Subject("���", 2008,null),
            new Subject("����������", 2008,null),
            new Subject("�������", 2007,null)
        };
    }
    /// <summary>
    /// �������� ������ �� 8 ���������
    /// </summary>
    /// <returns></returns>
    private List<Student> CreateListStudents()
    {
        return new List<Student>()
    {
        new Student("�������", "�������", "����������", "2001-11111", new Class(11, '�',null), DateTime.Parse("2001/1/10"),null),
        new Student("�������", "����������", "���������", "2001-11111", new Class(10, '�',null), DateTime.Parse("2000/1/11"),null),
        new Student("�������", "������", "����������", "2001-11111", new Class(9, '�',null), DateTime.Parse("2001/2/16"),null),
        new Student("�������", "������", "����������", "2001-11111", new Class(10, '�',null), DateTime.Parse("1999/1/10"),null),
        new Student("�������", "�������", "����������", "2001-11111", new Class(11, '�',null), DateTime.Parse("2001/2/10"),null),
        new Student("�������", "�������", "����������", "2001-11111", new Class(11, '�',null), DateTime.Parse("2003/4/10"),null),
        new Student("����������", "�������", "����������", "2001-11111", new Class(8, '�',null), DateTime.Parse("2005/11/10"),null),
        new Student("��������", "�������", "����������", "2001-11111", new Class(9, '�',null), DateTime.Parse("2008/1/10"),null)
    };
    }

    /// <summary>
    /// 1) ������� ���������� ��� ���� ���������. �������� �� ����������� ���������
    /// </summary>
    [Fact]
    public void AllSubjectTest()
    {
        var subjects = CreateListSubjects();
        Assert.Equal(8, subjects.Count);
    }

    /// <summary>
    /// 2) ������� ���������� ��� ���� �������� � ��������� ������, ����������� �� ���.
    /// </summary>
    [Fact]
    public void AllStudentClassTest()
    {
        var students = CreateListStudents();
        var @class = new Class(11, '�',null);

        var needStudents = (from student in students
                            where student.Class != null && student.Class.Equals(@class)
                            orderby student.LastName, student.FirstName, student.Patronymic
                            select student).ToList();

        var result = new List<Student>
        {
            new Student("�������", "�������", "����������", "2001-11111", new Class(11, '�',null), DateTime.Parse("2001/1/10"),null),
            new Student("�������", "�������", "����������", "2001-11111", new Class(11, '�',null), DateTime.Parse("2001/2/10"),null),
            new Student("�������", "�������", "����������", "2001-11111", new Class(11, '�',null), DateTime.Parse("2003/4/10"),null)
        };

        for (var i = 0; i < result.Count; i++)
        {
            Assert.True(result[i].Equals(needStudents.ElementAt(i)));
        }
    }

    /// <summary>
    /// 3) ������� ���������� ��� ���� ��������, ���������� ������ � ��������� ����.
    /// </summary>
    [Fact]
    public void StudentGradesDayTest()
    {
        var grades = CreateListGrades();

        var info = (from grade in grades
                    where grade.Date == DateTime.Parse("2022/10/10")
                    select grade.Student).ToList();

        Assert.Equal(5, info.Count);
    }

    /// <summary>
    /// 4) ������� ��� 5 �������� �� �������� �����.
    /// </summary>
    [Fact]
    public void TopStudentsAvrMarkTest()
    {
        var grades = CreateListGrades();

        var topFive = (from grade in grades
                       group grade by grade.Student into g
                       select new
                       {
                           Student = g.Key,
                           Marks = g.Average(s => s.Mark)
                       }).Take(5).OrderByDescending(s => s.Marks).ThenBy(s => s.Student.FirstName).ToList();

        var result = new List<Student>
        {
            new Student("�������", "������", "����������", "2001-11111", new Class(9, '�',null), DateTime.Parse("2001/2/16"),null),
            new Student("�������", "����������", "���������", "2001-11111", new Class(10, '�',null), DateTime.Parse("2000/1/11"),null),
            new Student("�������", "������", "����������", "2001-11111", new Class(10, '�',null), DateTime.Parse("1999/1/10"),null),
            new Student("�������", "�������", "����������", "2001-11111", new Class(11, '�',null), DateTime.Parse("2001/2/10"),null),
            new Student("�������", "�������", "����������", "2001-11111", new Class(11, '�',null), DateTime.Parse("2001/1/10"),null)
        };

        for (var i = 0; i < result.Count; i++)
        {
            Assert.True(result[i].Equals(topFive.ElementAt(i).Student));
        }

    }

    /// <summary>
    /// 5) ������� �������� � ������������ ������� ������ �� ��������� ������.
    /// </summary>
    [Fact]
    public void MaxAvrScorePeriodTest()
    {
        var grades = CreateListGrades();

        var maxValue = (from grade in grades
                        where grade.Date > DateTime.Parse("2022/10/10") && grade.Date <= DateTime.Parse("2022/10/14")
                        select grade.Mark).Max();

        var maxMark = (from grade in grades
                       where grade.Date > DateTime.Parse("2022/10/10") && grade.Date <= DateTime.Parse("2022/10/14")
                       group grade by grade.Student into g
                       select new
                       {
                           Student = g.Key,
                           Marks = g.Average(s => s.Mark)
                       }).Where(s => s.Marks == maxValue).ToList();

        Assert.True(maxMark.Count == 4);
    }

    /// <summary>
    /// 6) ������� ���������� � �����������, ������� � ������������ ����� �� ������� ��������.
    /// </summary>
    [Fact]
    public void MinMaxAvrMarkSubjectTest()
    {
        var grades = CreateListGrades();

        var infoMarks = (from grade in grades
                         group grade by grade.Subject into g
                         select new
                         {
                             Min = g.Min(s => s.Mark),
                             Max = g.Max(s => s.Mark),
                             Average = g.Average(s => s.Mark)
                         }).ToList();

        Assert.Equal(3, infoMarks[1].Min);
        Assert.Equal(4, infoMarks[1].Max);
        Assert.Equal(3.8, infoMarks[1].Average);
    }
}