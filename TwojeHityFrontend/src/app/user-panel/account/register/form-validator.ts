import { AbstractControl, ValidationErrors } from '@angular/forms';

export class FormValidators {
    static mustMatch(controlName: string, checkControlName: string) {
        return (control: AbstractControl) => {
          const password = control.get(controlName);
          const checkControl = control.get(checkControlName);
          if (checkControl?.errors && !checkControl.errors['mustMatch']) {
            return null;
          } 
          if (password?.value !== checkControl?.value) {
            checkControl?.setErrors({ mustMatch: true });
            return { mustMatch: true };
          } else {
           checkControl?.setErrors(null);
            return null;
          }
        };
      }
} 

