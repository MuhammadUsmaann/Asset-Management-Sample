/* eslint-disable @typescript-eslint/no-explicit-any */


import { useRef, useState } from 'react'
import { createPortal } from 'react-dom'
import { Modal } from 'react-bootstrap'
import { KTIcon } from '../../../helpers'
import * as Yup from 'yup'
import { useFormik } from 'formik'
import clsx from 'clsx'

const initialValues = {
  firstname: '',
  lastname: '',
  email: '',
  team: '',
}

const createUserSchema = Yup.object().shape({
  firstname: Yup.string()
    .min(3, 'Minimum 3 symbols')
    .max(50, 'Maximum 50 symbols')
    .required('First name is required'),
  email: Yup.string()
    .email('Wrong email format')
    .min(3, 'Minimum 3 symbols')
    .max(50, 'Maximum 50 symbols')
    .required('Email is required'),
  lastname: Yup.string()
    .min(3, 'Minimum 3 symbols')
    .max(50, 'Maximum 50 symbols')
    .required('Last name is required'),
  team: Yup.string()
    .required('Last name is required'),
})


type Props = {
  show: boolean
  handleClose: () => void
}

const modalsRoot = document.getElementById('root-modals') || document.body

const CreateUserModal = ({ show, handleClose }: Props) => {
  const stepperRef = useRef<HTMLDivElement | null>(null)

  const [loading, setLoading] = useState(false)
  const formik = useFormik({
    initialValues,
    validationSchema: createUserSchema,
    onSubmit: async (values, { setSubmitting }) => {
      setLoading(true)
      try {
        // await register(
        //   values.email,
        //   values.firstname,
        //   values.lastname,
        //   values.password,
        //   values.changepassword
        // )
      } catch (error) {
        console.error(error)
        setSubmitting(false)
        setLoading(false)
      }
    },
  })


  return createPortal(
    <Modal
      tabIndex={-1}
      aria-hidden='true'
      dialogClassName='modal-dialog modal-dialog-centered mw-900px'
      show={show}
      onHide={handleClose}
      backdrop={true}
    >
      <div className='modal-header'>
        <h2>Create User</h2>
        {/* begin::Close */}
        <div className='btn btn-sm btn-icon btn-active-color-primary' onClick={handleClose}>
          <KTIcon className='fs-1' iconName='cross' />
        </div>
        {/* end::Close */}
      </div>

      <div className='modal-body py-lg-10 px-lg-10'>
        {/*begin::Stepper */}
        <div
          ref={stepperRef}
          className='stepper stepper-pills stepper-column d-flex flex-column flex-xl-row flex-row-fluid'
        >
          {/*begin::Content */}
          <form
            className='form w-100 fv-plugins-bootstrap5 fv-plugins-framework'
            onSubmit={formik.handleSubmit}
          >

            {formik.status && (
              <div className='mb-lg-15 alert alert-danger'>
                <div className='alert-text font-weight-bold'>{formik.status}</div>
              </div>
            )}

            {/* begin::Form group Firstname */}
            <div className='fv-row mb-8'>
              <label className='form-label fw-bolder text-gray-900 fs-6'>First name</label>
              <input
                placeholder='First name'
                type='text'
                autoComplete='off'
                {...formik.getFieldProps('firstname')}
                className={clsx(
                  'form-control bg-transparent',
                  {
                    'is-invalid': formik.touched.firstname && formik.errors.firstname,
                  },
                  {
                    'is-valid': formik.touched.firstname && !formik.errors.firstname,
                  }
                )}
              />
              {formik.touched.firstname && formik.errors.firstname && (
                <div className='fv-plugins-message-container'>
                  <div className='fv-help-block'>
                    <span role='alert'>{formik.errors.firstname}</span>
                  </div>
                </div>
              )}
            </div>
            {/* end::Form group */}
            <div className='fv-row mb-8'>
              {/* begin::Form group Lastname */}
              <label className='form-label fw-bolder text-gray-900 fs-6'>Last name</label>
              <input
                placeholder='Last name'
                type='text'
                autoComplete='off'
                {...formik.getFieldProps('lastname')}
                className={clsx(
                  'form-control bg-transparent',
                  {
                    'is-invalid': formik.touched.lastname && formik.errors.lastname,
                  },
                  {
                    'is-valid': formik.touched.lastname && !formik.errors.lastname,
                  }
                )}
              />
              {formik.touched.lastname && formik.errors.lastname && (
                <div className='fv-plugins-message-container'>
                  <div className='fv-help-block'>
                    <span role='alert'>{formik.errors.lastname}</span>
                  </div>
                </div>
              )}
              {/* end::Form group */}
            </div>

            {/* begin::Form group Email */}
            <div className='fv-row mb-8'>
              <label className='form-label fw-bolder text-gray-900 fs-6'>Email</label>
              <input
                placeholder='Email'
                type='email'
                autoComplete='off'
                {...formik.getFieldProps('email')}
                className={clsx(
                  'form-control bg-transparent',
                  { 'is-invalid': formik.touched.email && formik.errors.email },
                  {
                    'is-valid': formik.touched.email && !formik.errors.email,
                  }
                )}
              />
              {formik.touched.email && formik.errors.email && (
                <div className='fv-plugins-message-container'>
                  <div className='fv-help-block'>
                    <span role='alert'>{formik.errors.email}</span>
                  </div>
                </div>
              )}
            </div>
            {/* end::Form group */}

            <div className='fv-row mb-8'>
              {/* begin::Form group Lastname */}
              <label className='form-label fw-bolder text-gray-900 fs-6'>Team</label>
              <select
                className='form-select form-select-solid form-select-lg fw-bold'
                {...formik.getFieldProps('team')}
              >
                <option value=''>Select a Team...</option>
                <option value='A'>Team A</option>
                <option value='B'>Team B</option>
                <option value='C'>Team C</option>
                <option value='D'>Team D</option>

              </select>
              {formik.touched.team && formik.errors.team && (
                <div className='fv-plugins-message-container'>
                  <div className='fv-help-block'>{formik.errors.team}</div>
                </div>
              )}
              {/* end::Form group */}
            </div>


            {/* begin::Form group */}
            <div className='text-center'>
              <button
                type='submit'
                id='kt_sign_up_submit'
                className='btn btn-lg btn-primary w-100 mb-5'
                disabled={formik.isSubmitting || !formik.isValid || !formik.values.team}
              >
                {!loading && <span className='indicator-label'>Submit</span>}
                {loading && (
                  <span className='indicator-progress' style={{ display: 'block' }}>
                    Please wait...{' '}
                    <span className='spinner-border spinner-border-sm align-middle ms-2'></span>
                  </span>
                )}
              </button>
            </div>
            {/* end::Form group */}
          </form>
          {/*end::Content */}
        </div>
        {/* end::Stepper */}
      </div>
    </Modal>,
    modalsRoot
  )
}

export { CreateUserModal }
