import { Component, OnInit, signal } from '@angular/core';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzInputModule } from 'ng-zorro-antd/input';
import { formatDistance } from 'date-fns';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { UserService } from '../../service/userData.service';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { ThisReceiver } from '@angular/compiler';

@Component({
  selector: 'app-comments',
  standalone: true,
  imports: [
    NzCommentModule,
    NzFormModule,
    NzButtonModule,
    ReactiveFormsModule,
    NzListModule,
    FormsModule,
    NzAvatarModule,
    NzInputModule,
    NzTabsModule
  ],
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {
  commentsForm: FormGroup;
  userValidId = signal(0);
  replayId=signal(0);

  data: any[] = [];
  replaydata: any[] = [];
  submitting = false;
  user = {
    author: 'Han Solo',
    avatar: 'https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png'
  };

  activeSection: 'upcoming' | 'completed' = 'upcoming';
  isDisplay=signal(true);
  setActive(section: 'upcoming' | 'completed') {
    this.activeSection = section;
    switch(this.activeSection){
      case 'upcoming':
            this.isDisplay.set(true);
            break;

      case 'completed':
            this.isDisplay.set(false);
            break;
        
    }
  }

  constructor(private fb: FormBuilder, private userService: UserService) {
    const user = sessionStorage.getItem('valid-user');
    if (user) {
      const userId = JSON.parse(user);
      this.userValidId.set(userId.id);
    }
    this.commentsForm = fb.group({
      id: 0,
      userId: this.userValidId(),
      name: [''],
      comment: ['',Validators.required],
      createDate:[Date.now]

    });
  }

  ngOnInit(): void {
    this.GetComments();
  }

  GetComments() {
    this.userService.GetCommentById(this.userValidId()).subscribe({
      next: (response: any) => {
        if (response.isSuccess) {
          this.data = response.value.map((comment: any) => {
            const datetime = new Date(comment.createDate);
             this.replayId.set(comment.replayComment);
            const displayTime = isNaN(datetime.getTime()) ? 'Invalid date' : formatDistance(datetime, new Date(), { addSuffix: true });
            return {
              ...comment,
              displayTime,
              showReply: false,
              replyForm: new FormGroup({
                reply: new FormControl('')
              }),
              replies: comment.replies || []
            };
          });
          console.log('comments', this.data);
        }
      }
    });

    this.userService.GetCommentByCommentId(this.userValidId(),this.replayId()).subscribe({
      next: (response: any) => {
        if (response.isSuccess) {
          this.replaydata = response.value.map((comment: any) => {
            const datetime = new Date(comment.createDate);
            const displayTime = isNaN(datetime.getTime()) ? 'Invalid date' : formatDistance(datetime, new Date(), { addSuffix: true });
            return {
              ...comment,
              displayTime,
              showReply: false,
              replyForm: new FormGroup({
                reply: new FormControl('')
              }),
              replies: comment.replies || []
            };
          });
          console.log('comments', this.data);
        }
      }
    });
  }

  handleSubmit(): void {
    this.submitting = true;
    const content = this.commentsForm.get('comment')?.value;
    if (this.commentsForm.valid) {
      console.log('formsubmit==>',this.commentsForm.value);

      this.userService.Comments(this.commentsForm.value).subscribe({
        next: (response: any) => {
          if (response.isSuccess) {
            this.GetComments();
            this.commentsForm.patchValue({ comment: '' });
          }
        }
      });

      setTimeout(() => {
        this.submitting = false;
        this.data = [
          ...this.data,
          {
            ...this.user,
            content,
            datetime: new Date(),
            displayTime: formatDistance(new Date(), new Date(), { addSuffix: true })
          }
        ].map(e => ({
          ...e,
          displayTime: formatDistance(new Date(e.datetime), new Date(), { addSuffix: true })
        }));
      }, 800);
    }else {
      Object.values(this.commentsForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleReply(comment: any): void {
    const replyContent = comment.replyForm.get('reply')?.value;

    if (comment.replyForm.valid) {
      const reply = {
        author: this.user.author,
        avatar: this.user.avatar,
        content: replyContent,
        datetime: new Date(),
        displayTime: formatDistance(new Date(), new Date(), { addSuffix: true })
      };
      const commentReplay= comment.replyForm.patchValue({ reply: '' });
      comment.replies.push(reply);
      const replayForm:any={
       id:0,
       userId:this.userValidId(),
       name:'',
       comment: replyContent,
       createDate:Date.now,
       replayComment: comment.id
      }
      console.log('ddddd',replayForm);
      
      // comment.replyForm.patchValue({ reply: '' }); // Reset only the reply field
      // console.log(comment.id);
      
      // Optionally, you can save the reply to the server
      this.userService.ReplayComments(replayForm).subscribe({
        next: (response: any) => {
          if (response.isSuccess) {
            console.log('Reply saved successfully');
          }
        }
      });
    }
  }
}
