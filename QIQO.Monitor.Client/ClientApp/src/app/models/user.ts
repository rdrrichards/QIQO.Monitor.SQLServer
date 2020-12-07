export interface User {
  userInits: string;
  userName: string;
  isSupervisor: boolean;
  roles: Role[];
  workLocationCode?: string;
}

export interface Role {
  roleName: string;
  roleValue: string;
}
