using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest : IsuService
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var courseNumber = new CourseNumber('2');
        var groupName = new GroupName('M', '3', courseNumber, "90");
        AddGroup(groupName);
        Group? group = FindGroup(groupName);
        if (group == null) return;
        var vova = new Student("vova", group);

        Assert.Equal(group, vova.GetGroup());
        Assert.Contains(vova, group.GetStudents());
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var courseNumber = new CourseNumber('2');
        var groupName = new GroupName('M', '3', courseNumber, "90");
        AddGroup(groupName);
        Group? group = FindGroup(groupName);
        const string name = "IVAN";
        Assert.Throws<StudentException>(
            () =>
            {
                for (int i = 0; i < 30; i++)
                {
                    if (group != null) AddStudent(group, name);
                }
            });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<GroupNameException>(
            () =>
            {
                var courseNumber = new CourseNumber('1');
                var groupName = new GroupName('@', '3', courseNumber, "32");
            });
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var courseNumber = new CourseNumber('2');
        var groupName1 = new GroupName('M', '3', courseNumber, "1");
        AddGroup(groupName1);
        var groupName2 = new GroupName('M', '3', courseNumber, "2");
        AddGroup(groupName2);
        Group? group1 = FindGroup(groupName1);
        Group? group2 = FindGroup(groupName2);
        if (group1 == null) return;
        var vova = new Student("vova", group1);
        if (group2 == null) return;
        ChangeStudentGroup(vova, group2);
        Assert.Equal(group2, vova.GetGroup());
        Assert.Contains(vova, group2.GetStudents());
    }
}