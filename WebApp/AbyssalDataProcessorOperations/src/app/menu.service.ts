import { Injectable } from '@angular/core';
import { MenuItem } from './menu-item';
import { MENU_ITEMS } from './mock-menu-items'

@Injectable()
export class MenuService {

  constructor() { }

  getMenuItems() : Promise<Array<Array<MenuItem>>> {
    let items = JSON.parse(localStorage.getItem('adpMenuItems'));
    if (items) {
      return Promise.resolve(items as Array<Array<MenuItem>>);
    }
    else {
      localStorage.setItem('adpMenuItems', JSON.stringify(MENU_ITEMS));
      return Promise.resolve(MENU_ITEMS);
    }    
  }
}
