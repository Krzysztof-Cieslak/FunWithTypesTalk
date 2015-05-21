class PostalAdress : IValue
{
    public PostalAdress(string street, string city, string postalCode)
    {
        this.Street = street;
        this.City = city;
        this.PostalCode = postalCode
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }

    public override int GetHashCode()
    {
        return this.Street.GetHashCode() + this.City.GetHashCode() + this.PostalCode.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return Equals(other as PostalAdress);
    }

    public bool Equals(PostalAdress other)
    {
        if ((object) other == null)
        {
            return false;
        }
        return City == other.City && Street == other.Street && PostalCode == other.PostalCode;
    }
}
