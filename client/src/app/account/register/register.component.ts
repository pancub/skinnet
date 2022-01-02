import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  errors: string[];
  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    /*
    this.registerForm = new FormGroup(
      {
        email : new FormControl('',[Validators.required,Validators.pattern('^[\\w-\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
        password : new FormControl('',Validators.required),
        displayName : new FormControl('',Validators.required),
      }
    )
    */
    this.registerForm = this.fb.group(
      {
        email: [null, 
                [Validators.required, Validators.pattern('^[\\w-\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
                [this.validEmailNotTaken()]
               ],
        password: [null, Validators.required],
        displayName: [null, Validators.required]
      }
    );
  }
  onSubmit() {
    //console.log(this.registerForm.value);
    this.accountService.register(this.registerForm.value).subscribe(
      () => {
        this.router.navigateByUrl('/shop');
      },
      error => {
        console.log(error);
        this.errors = error.errors;
      }

    );
  }

  validEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.checkEmailExists(control.value).pipe(
            map(res => {
              return res ? { emailExists: true } : null;
            })
          );
        })
      );
    }
  };

}
