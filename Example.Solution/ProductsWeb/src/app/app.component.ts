import { Component, OnInit } from '@angular/core';
import { ProductService } from './shared/product.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  constructor(public service: ProductService){}

  msgTitle = '';
  msgDescription = '';

  FileName: string = "Seleccionar imagen";
  FileToUpload: File | null = null;
  ImageURL: string = "assets/img/no-imagen.jpg";

  public FileUploadControl(e: Event): void{
    this.FileToUpload = (e.target as HTMLInputElement).files?.item(0) as File;
    this.FileName = this.FileUploadControl.name;
    var reader = new FileReader();

    reader.onload = (event:any) => {
      this.ImageURL = event.target.result;
    }

    reader.readAsDataURL(this.FileToUpload);
  }

  resetForm(form?:NgForm){
    if(form != null)
      this.resetForm();

    this.service.formData = {
      IdProduct: 0,
      ProductName: '',
      Price: 0,
      Description: '',
      Image: ''
    };

    this.ImageURL = "assets/img/no-imagen.jpg";
  }

  onSubmit(form: NgForm){
    if(this.FileToUpload != null){
      this.service.postProduct(form.value, this.FileToUpload).subscribe(
        res => {
          this.resetForm(form);
          this.ngOnInit();
          this.msgTitle = 'Producto agregado';
          this.msgDescription = 'Producto agregado correctamente';
        },
        err => {
          this.resetForm(form);
          this.msgTitle = 'error en el proceso';
          this.msgDescription = err.error();
        }
      )
    }
  }

  ngOnInit(): void {
    this.service.getProducts();
  }

  onDelete(id:number){
    if(confirm("Está seguro de eliminar este producto?")){
      this.service.deleteProduct(id).subscribe(
        res => {
          this.ngOnInit();
          this.msgTitle = 'producto eliminado';
          this.msgDescription = 'producto eliminado correctamente';
        },
        err => {
          this.msgTitle = 'error en el proceso';
          this.msgDescription = err.error();
        }
      );
    }
  }
}
