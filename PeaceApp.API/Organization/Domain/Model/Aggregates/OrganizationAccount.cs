using PeaceApp.API.Organization.Domain.Model.Commands;

namespace PeaceApp.API.Organization.Domain.Model.Aggregates;

public class OrganizationAccount
{
    public int Id { get; private set; }
    //be careful wit this 
    //Mayusculas??
    public string OrganizationName { get; private set; }
    public string Location { get; private set; }
    public string Cellphone { get; private set; }
    protected OrganizationAccount()
    {
        this.OrganizationName = string.Empty;
        this.Location = string.Empty;
        this.Cellphone = string.Empty;
    }

    public OrganizationAccount(CreateOrganizationAccountCommand command)
    {
        this.OrganizationName = command.OrganizationName;
        this.Location = command.Location;
        this.Cellphone = command.Cellphone; 
    }
}