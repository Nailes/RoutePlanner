import { Component, OnInit, Injectable } from '@angular/core';
import { ListPageService } from "../../_services/list-page.service";
import { Routes } from "../../../models/routes.model"

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
  providers: [ListPageService]
})
export class MenuComponent implements OnInit {

  constructor(private ListPageService: ListPageService) { }
  list: Routes[] = [];
  ngOnInit() {
    this.ListPageService.getListAny().subscribe(list => {
      this.list = list["RouteList"];
    })
  }
}
