import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-key-point-form',
  templateUrl: './key-point-form.component.html',
  styleUrls: ['./key-point-form.component.css']
})
export class KeyPointFormComponent {

  image: string | ArrayBuffer | null = null; // Preview of the image

  constructor(
    public dialogRef: MatDialogRef<KeyPointFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  onSave(): void {
    this.dialogRef.close(this.data);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.data.filepath = file.name;
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.data.image = e.target.result;
      };
      reader.readAsDataURL(file)
    } else {
      this.data.imageName = null;
    }
  }
}
