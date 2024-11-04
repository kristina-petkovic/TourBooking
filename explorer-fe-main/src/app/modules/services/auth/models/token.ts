export class Token {
  sub: string;
  nameid: string;
  role: string;
  authorities: string[];
  iat: number;
  exp: number;

  constructor(sub: string, id: string, authorities: string[], iat: number, exp: number, role: string) {
    this.sub = sub;
    this.nameid = id;
    this.authorities = authorities;
    this.iat = iat;
    this.exp = exp;
    this.role = role;
  }
}
