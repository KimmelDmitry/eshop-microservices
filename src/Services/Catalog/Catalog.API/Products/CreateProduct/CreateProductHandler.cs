namespace Catalog.API.Products.CreateProduct;

//Request Dto
public record CreateProductCommand(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;

//Response Dto
public record CreateProductResult(Guid Id);


internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //create Product entity from command object
        var product = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        //save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);   
        //return result (CreateProductResult)   

        return new CreateProductResult(product.Id);
    }
}

