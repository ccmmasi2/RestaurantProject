import { Injectable } from '@angular/core';
import { Product } from './product.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class ProductService {
  public formData: Product = new Product();
  productList: Product[] = [];

  constructor(private httpClient: HttpClient) { }

  get ProductName(): string {
    return this.formData.ProductName;
  }
  set ProductName(ProductName: string){
    this.formData.ProductName = ProductName;
  }

  get Price(): number {
    return this.formData.Price;
  }
  set Price(Price: number){
    this.formData.Price = Price;
  }

  get Description(): string {
    return this.formData.Description;
  }
  set Description(Description: string){
    this.formData.Description = Description;
  }

  get Image(): string {
    return this.formData.Image;
  }
  set Image(Image: string){
    this.formData.Image = Image;
  }

  readonly rootURL = "https://localhost:7003/";

  postProduct(formData: Product, fileToUpload: File){
    let formToPost = new FormData();
    
    let requestToPost = JSON.stringify({
      ProductName: formData.ProductName,
      Price: formData.Price,
      Description: formData.Description,
      Image: fileToUpload.name
    });

    formToPost.append("product", requestToPost);
    formToPost.append("image", fileToUpload, fileToUpload.name);

    return this.httpClient.post(this.rootURL + "Products", formToPost);
  }

  getProducts(){
    this.httpClient.get(this.rootURL + "Products")
    .toPromise()
    .then(
      res => {
        this.productList = res as Product[];
      }
    )
  }

  deleteProduct(id:number){
    return this.httpClient.delete(this.rootURL + "Products/" + id);
  }
} 