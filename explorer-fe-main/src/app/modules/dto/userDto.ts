export class UserDto {
  id: number = 0;
  password: string = '';
  lastName: string = '';
  firstName: string = '';
  email: string = '';
  deleted: boolean = false;
  usertype: number = 0;
  primaryCareDoctorId: number = 1;
  cancelCount: number = 0;
  blocked: boolean = false;
  gender: number = 0;
  enabled: boolean = true;
  specialization: number = 0;

  public constructor(obj?: any) {
    if (obj) {
      this.id = obj.id;
      this.password = obj.password;
      this.lastName = obj.lastName;
      this.firstName = obj.firstName;
      this.email = obj.email;
      this.deleted = obj.deleted;
      this.usertype = obj.usertype;
      this.primaryCareDoctorId = obj.primaryCareDoctorId;
      this.cancelCount = obj.cancelCount;
      this.blocked = obj.blocked;
      this.gender = obj.gender;
      this.enabled = obj.enabled;
      this.specialization = obj.specialization;
    }
  }
}
export enum UserType {
  TOURIST = 'TOURIST',
  AUTHOR = 'AUTHOR',
  ADMIN = 'ADMIN',
}
