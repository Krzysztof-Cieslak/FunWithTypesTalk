class Order : IEntity
{
    public Order(int id, PostalAdress adress)
    {
        this.Id = id;
        this.Adress = adress;
    }
    
    public int Id { get; private set; }
    public PostalAdress Adress { get; set; }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return Equals(other as Person);
    }

    public bool Equals(Person other)
    {
        if ((object) other == null)
        {
            return false;
        }
        return Id == other.Id;
    }
}
