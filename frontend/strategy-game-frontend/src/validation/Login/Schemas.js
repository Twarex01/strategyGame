import * as yup from 'yup'

const validationSchema = yup.object({
  email: yup
    .string('Please enter your email address')
    .email('Please enter a valid email address')
    .required("Required field"),
  password: yup
    .string()
    .required('Please Enter your password')
    .matches(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{6,})/,
      "Must Contain 6 Characters, One Uppercase and One Lowercase Character"
    ),
});

export default validationSchema