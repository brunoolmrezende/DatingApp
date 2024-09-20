import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../_services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountServices = inject(AccountService);

  if (accountServices.currentUser()) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accountServices.currentUser()?.token}`
      }
    })
  }

  return next(req);
};
